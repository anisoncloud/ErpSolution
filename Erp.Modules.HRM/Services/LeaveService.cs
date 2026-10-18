using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Enums;
using Erp.Modules.HRM.Repositories;
using Erp.Modules.HRM.Settings;
using Erp.Modules.HRM.StaticData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public sealed class LeaveService : ILeaveService
    {
        private readonly IHrmUnitOfWork _uow;
        private readonly ILeaveDayCalculator _calculator;
        private readonly IHrmClock _clock;
        private readonly HrmSettings _settings;

        public LeaveService(IHrmUnitOfWork uow, ILeaveDayCalculator calculator, IHrmClock clock, HrmSettings settings)
        {
            _uow = uow;
            _calculator = calculator;
            _clock = clock;
            _settings = settings;
        }

        private static bool IsLifetime(LeavePolicy p) => p.LifetimeMaxTimes > 0;
        private static bool IsYearlyAllowance(LeavePolicy p) => p.LifetimeMaxTimes == 0 && p.YearlyDays > 0;

        private static LeavePreview Fail(params string[] errors) =>
            new(null, null, new List<YearBalance>(), errors);


        // ----------------------------------------------------------- lookups

        public Task<EmployeeRef?> GetEmployeeByUserAsync(Guid userId) => _uow.Employees.GetRefByUserIdAsync(userId);
        public Task<EmployeeRef?> GetEmployeeAsync(int employeeId) => _uow.Employees.GetRefAsync(employeeId);
        public async Task<IReadOnlyList<EmployeeRef>> GetEmployeesAsync() => await _uow.Employees.GetRefsAsync();
        public async Task<IReadOnlyList<EmployeeRef>> GetTeamAsync(int managerEmployeeId) => await _uow.Employees.GetTeamRefsAsync(managerEmployeeId);

        public async Task<IReadOnlyList<LeavePolicyDto>> GetApplicableTypesAsync(int employeeId)
        {
            var employee = await _uow.Employees.GetRefAsync(employeeId);
            if (employee is null) return Array.Empty<LeavePolicyDto>();
            return (await _uow.LeavePolicies.GetAllPoliciesAsync())
                .Where(p => p.LeaveType.IsApplicable() && LeavePolicyRules.IsEligible(p.RequiredGender, employee.Gender))
                .Select(LeavePolicyService.ToDto)
                .ToList();
        }

        // ------------------------------------------------------------- apply

        public async Task<LeavePreview> PreviewAsync(int employeeId, LeaveType type, DateOnly from, DateOnly? returnInput)
        {
            var employee = await _uow.Employees.GetRefAsync(employeeId);
            var policy = await _uow.LeavePolicies.GetByTypeAsync(type);
            if (employee is null || policy is null || !type.IsApplicable())
                return Fail("This leave type is not available.");
            if (!LeavePolicyRules.IsEligible(policy.RequiredGender, employee.Gender))
                return Fail($"{type} leave is only available to {policy.RequiredGender} employees.");

            var errors = new List<string>();

            var earliest = _clock.Today.AddDays(-_settings.ApplicationWindowDays);
            if (from < earliest)
                errors.Add($"The application window has passed. Leave must be applied within {_settings.ApplicationWindowDays} days; " +
                           $"the From date can be no earlier than {earliest:dd MMM yyyy}.");

            // Fixed-day leave: the return date is calculated, whatever the client sent.
            DateOnly returnDate;
            if (policy.FixedDays > 0)
            {
                returnDate = LeavePolicyRules.ReturnDateFor(from, policy.FixedDays);
            }
            else if (returnInput is null)
            {
                errors.Add("Return date is required.");
                returnDate = from;
            }
            else
            {
                returnDate = returnInput.Value;
                if (returnDate <= from) errors.Add("Return date must be after the From date.");
            }

            if (errors.Count > 0) return new LeavePreview(null, null, new List<YearBalance>(), errors);

            var lastDay = returnDate.AddDays(-1);
            var publicHolidays = await _uow.Holidays.GetDatesAsync(from, lastDay);
            var rules = new LeaveCountingRules(policy.CountAllCalendarDays, policy.SplitByYear);
            var result = _calculator.Calculate(type, from, returnDate, _settings.WeeklyHolidaySet, publicHolidays, rules);

            if (result.TotalDays == 0)
            {
                errors.Add("The selected range contains no working days, so there is nothing to apply for.");
                return new LeavePreview(result, returnDate, new List<YearBalance>(), errors);
            }

            if (policy.MinDays > 0 && result.TotalDays < policy.MinDays)
                errors.Add($"{type} leave must be at least {policy.MinDays} days (this application is {result.TotalDays}).");

            var balances = new List<YearBalance>();

            if (IsLifetime(policy))
            {
                var life = await _uow.LeaveApplications.GetLifetimeUsageAsync(employeeId, type);
                var times = life.ApprovedTimes + life.PendingTimes;
                var allocated = policy.FixedDays * policy.LifetimeMaxTimes;
                var remaining = allocated - life.ApprovedDays - life.PendingDays;

                if (times >= policy.LifetimeMaxTimes)
                    errors.Add($"{type} leave can be taken only {policy.LifetimeMaxTimes} time(s) in your job life, and you have already used {times}.");

                balances.Add(new YearBalance(from.Year, "Job life", allocated, life.ApprovedDays, life.PendingDays,
                    remaining, remaining - result.TotalDays, 0));
            }
            else
            {
                foreach (var y in result.ByYear)
                {
                    var entitlement = await _uow.LeaveEntitlements.GetAsync(employeeId, y.Year, type);
                    if (entitlement is null)
                    {
                        errors.Add(type == LeaveType.Compensation
                            ? $"No Compensation leave has been granted to you for {y.Year}."
                            : $"No {type} leave entitlement is set for {y.Year}. Please contact the Admin.");
                        continue;
                    }

                    var usage = (await _uow.LeaveApplications.GetUsageAsync(employeeId, y.Year)).FirstOrDefault(u => u.Type == type);
                    var approved = usage?.ApprovedDays ?? 0;
                    var pending = usage?.PendingDays ?? 0;
                    var cfUsed = (usage?.ApprovedCf ?? 0) + (usage?.PendingCf ?? 0);

                    // pending requests reserve days; CF-funded days are not part of the year's own allowance
                    var yearRemaining = Math.Max(0, entitlement.TotalDays - (approved + pending - cfUsed));

                    var cfTotal = 0;
                    var cfRemaining = 0;
                    if (type == LeaveType.Earned && policy.CarryForwardCap > 0)
                    {
                        var cf = await _uow.LeaveEntitlements.GetAsync(employeeId, y.Year, LeaveType.EarnedCarryForward);
                        cfTotal = cf?.TotalDays ?? 0;
                        cfRemaining = Math.Max(0, cfTotal - cfUsed);
                    }

                    // Draw on this year's Earned first, then on Earned CF.
                    var fromYear = Math.Min(y.Total, yearRemaining);
                    var fromCf = y.Total - fromYear;
                    var available = yearRemaining + cfRemaining;

                    if (fromCf > cfRemaining)
                    {
                        var cfNote = cfTotal > 0 ? $" ({yearRemaining} from {type} + {cfRemaining} from Earned CF)" : "";
                        errors.Add($"{type} leave in {y.Year}: you are applying for {y.Total} day(s) but only {available} remain{cfNote}.");
                    }

                    balances.Add(new YearBalance(y.Year, y.Year.ToString(), entitlement.TotalDays + cfTotal,
                        approved, pending, available, available - y.Total, fromCf));
                }
            }

            if (await _uow.LeaveApplications.HasOverlapAsync(employeeId, from, lastDay))
                errors.Add("These dates overlap with another Pending or Approved leave application.");

            return new LeavePreview(result, returnDate, balances, errors);
        }

        public async Task<SubmitResult> SubmitAsync(int employeeId, LeaveType type, DateOnly from, DateOnly? returnInput)
        {
            var preview = await PreviewAsync(employeeId, type, from, returnInput);
            if (!preview.IsValid) return new SubmitResult(preview, null);

            var result = preview.Result!;
            var application = new LeaveApplication
            {
                EmployeeId = employeeId,
                LeaveType = type,
                FromDate = from,
                ReturnDate = preview.ReturnDate!.Value,
                TotalDays = result.TotalDays,
                Status = LeaveStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                Years = result.ByYear.Select(y => new LeaveApplicationYear
                {
                    Year = y.Year,
                    Days = y.Total,
                    WorkingDays = y.Working,
                    BridgedWeeklyDays = y.BridgedWeekly,
                    BridgedPublicDays = y.BridgedPublic,
                    CarryForwardDays = preview.Balances.FirstOrDefault(b => b.Year == y.Year)?.FromCarryForward ?? 0
                }).ToList()
            };

            await _uow.LeaveApplications.AddAsync(application);
            await _uow.SaveChangesAsync();
            return new SubmitResult(preview, application.Id);
        }

        // ------------------------------------------------------------ report

        public async Task<LeaveReport> GetReportAsync(int employeeId, int year)
        {
            var employee = await _uow.Employees.GetRefAsync(employeeId);
            var policies = await _uow.LeavePolicies.GetAllPoliciesAsync();
            var entitlements = (await _uow.LeaveEntitlements.GetForEmployeeAsync(employeeId, year)).ToDictionary(e => e.LeaveType, e => e.TotalDays);
            var usage = (await _uow.LeaveApplications.GetUsageAsync(employeeId, year)).ToDictionary(u => u.Type);

            var balances = new List<LeaveTypeBalance>();
            foreach (var p in policies.Where(p => p.LeaveType.IsApplicable()
                                                  && LeavePolicyRules.IsEligible(p.RequiredGender, employee?.Gender)))
            {
                if (IsLifetime(p))
                {
                    var life = await _uow.LeaveApplications.GetLifetimeUsageAsync(employeeId, p.LeaveType);
                    var times = life.ApprovedTimes + life.PendingTimes;
                    balances.Add(new LeaveTypeBalance(p.LeaveType, $"Job life: {times} of {p.LifetimeMaxTimes} time(s) used",
                        p.FixedDays * p.LifetimeMaxTimes, life.ApprovedDays, life.PendingDays));
                    continue;
                }

                usage.TryGetValue(p.LeaveType, out var u);
                var cfUsedApproved = u?.ApprovedCf ?? 0;
                var cfUsedPending = u?.PendingCf ?? 0;

                CarryForwardBalance? cf = null;
                if (p.LeaveType == LeaveType.Earned && p.CarryForwardCap > 0)
                    cf = new CarryForwardBalance(entitlements.GetValueOrDefault(LeaveType.EarnedCarryForward), cfUsedApproved, cfUsedPending);

                // Earned CF is reported in its own column, so it is taken out of the Earned figures.
                balances.Add(new LeaveTypeBalance(p.LeaveType, $"Year {year}",
                    entitlements.GetValueOrDefault(p.LeaveType),
                    (u?.ApprovedDays ?? 0) - cfUsedApproved,
                    (u?.PendingDays ?? 0) - cfUsedPending,
                    cf));
            }

            var apps = (await _uow.LeaveApplications.GetApplicationsForYearAsync(employeeId, year))
                .Select(a => new LeaveReportItem(
                    a.Id, a.LeaveType, a.FromDate, a.ReturnDate, a.TotalDays,
                    a.Years.Where(y => y.Year == year).Sum(y => y.Days),
                    a.Status, a.ManagerComment, a.DecidedBy?.FullName, a.DecidedAt))
                .ToList();

            return new LeaveReport(year, balances, apps);
        }

        // ---------------------------------------------------------- approval

        public async Task<IReadOnlyList<PendingLeaveItem>> GetPendingForManagerAsync(int managerEmployeeId) =>
            (await _uow.LeaveApplications.GetPendingForManagerAsync(managerEmployeeId))
                .Select(a => new PendingLeaveItem(a.Id, a.Employee!.FullName, a.LeaveType, a.FromDate, a.ReturnDate, a.TotalDays))
                .ToList();

        public async Task<DecideResult> DecideAsync(int managerEmployeeId, IEnumerable<int> applicationIds, bool approve, string? comment)
        {
            var errors = new List<string>();
            comment = string.IsNullOrWhiteSpace(comment) ? null : comment.Trim();

            if (!approve && comment is null)
                return new DecideResult(0, new[] { "A comment is required when declining." });

            var apps = await _uow.LeaveApplications.GetApplicationsByIdsAsync(applicationIds.Distinct());
            var processed = 0;

            foreach (var app in apps)
            {
                // Server-side team check: only the assigned manager may act.
                if (app.Employee?.EmpManagerId != managerEmployeeId)
                {
                    errors.Add($"Application #{app.Id} does not belong to your team.");
                    continue;
                }
                if (app.Status != LeaveStatus.Pending)
                {
                    errors.Add($"Application #{app.Id} is no longer pending.");
                    continue;
                }

                app.Status = approve ? LeaveStatus.Approved : LeaveStatus.Declined;
                app.DecidedById = managerEmployeeId;
                app.DecidedAt = DateTime.UtcNow;
                app.ManagerComment = comment;
                app.UpdatedAt = DateTime.UtcNow;
                processed++;
            }

            if (processed > 0) await _uow.SaveChangesAsync();
            return new DecideResult(processed, errors);
        }

        // ----------------------------------------------------- entitlements

        private static readonly LeaveType[] ManagedTypes =
        {
        LeaveType.Casual, LeaveType.Sick, LeaveType.Earned, LeaveType.EarnedCarryForward, LeaveType.Compensation
    };

        private static int DefaultFor(LeaveType type, IReadOnlyDictionary<LeaveType, LeavePolicy> policies) =>
            type is LeaveType.EarnedCarryForward or LeaveType.Compensation
                ? 0
                : policies.TryGetValue(type, out var p) ? p.YearlyDays : 0;

        public async Task<Dictionary<LeaveType, int>> GetEntitlementTotalsAsync(int employeeId, int year)
        {
            var policies = (await _uow.LeavePolicies.GetAllPoliciesAsync()).ToDictionary(p => p.LeaveType);
            var existing = (await _uow.LeaveEntitlements.GetForEmployeeAsync(employeeId, year)).ToDictionary(e => e.LeaveType, e => e.TotalDays);
            return ManagedTypes.ToDictionary(t => t, t => existing.TryGetValue(t, out var v) ? v : DefaultFor(t, policies));
        }

        public async Task AssignEntitlementsAsync(int employeeId, int year, IReadOnlyDictionary<LeaveType, int>? totals = null)
        {
            var policies = (await _uow.LeavePolicies.GetAllPoliciesAsync()).ToDictionary(p => p.LeaveType);
            var existing = (await _uow.LeaveEntitlements.GetForEmployeeAsync(employeeId, year)).ToDictionary(e => e.LeaveType);
            var cap = policies.TryGetValue(LeaveType.Earned, out var earned) ? earned.CarryForwardCap : 0;
            var toAdd = new List<LeaveEntitlement>();

            foreach (var type in ManagedTypes)
            {
                var provided = totals is not null && totals.TryGetValue(type, out var t);
                var value = provided ? totals![type] : DefaultFor(type, policies);

                if (value < 0) throw new ArgumentOutOfRangeException(nameof(totals), $"{type.Display()} days cannot be negative.");
                if (type == LeaveType.EarnedCarryForward && value > cap)
                    throw new ArgumentOutOfRangeException(nameof(totals), $"Earned CF cannot exceed the limit of {cap} days.");

                if (existing.TryGetValue(type, out var row))
                {
                    if (!provided) continue; // keep what the Admin set earlier
                    row.TotalDays = value;
                    row.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    toAdd.Add(new LeaveEntitlement { EmployeeId = employeeId, Year = year, LeaveType = type, TotalDays = value, CreatedAt = DateTime.UtcNow });
                }
            }

            if (toAdd.Count > 0) await _uow.LeaveEntitlements.AddManyAsync(toAdd);
            await _uow.SaveChangesAsync();
        }

        public async Task GrantCompensationAsync(int managerEmployeeId, int employeeId, int year, int days, string reason)
        {
            if (days <= 0) throw new ArgumentException("Days must be greater than zero.", nameof(days));
            if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("A reason is required.", nameof(reason));
            var thisYear = _clock.Today.Year;
            if (year < thisYear || year > thisYear + 1)
                throw new ArgumentException($"Year must be {thisYear} or {thisYear + 1}.", nameof(year));

            var employee = await _uow.Employees.GetRefAsync(employeeId);
            if (employee is null || employee.ManagerId != managerEmployeeId)
                throw new InvalidOperationException("You can set Compensation leave only for your own team members.");

            var entitlement = await _uow.LeaveEntitlements.GetAsync(employeeId, year, LeaveType.Compensation);
            if (entitlement is null)
            {
                await _uow.LeaveEntitlements.AddManyAsync(new[]
                {
                new LeaveEntitlement { EmployeeId = employeeId, Year = year, LeaveType = LeaveType.Compensation, TotalDays = days, CreatedAt = DateTime.UtcNow }
            });
            }
            else
            {
                entitlement.TotalDays += days;
                entitlement.UpdatedAt = DateTime.UtcNow;
            }

            await _uow.CompensationGrants.AddAsync(new CompensationGrant
            {
                EmployeeId = employeeId,
                Year = year,
                Days = days,
                Reason = reason.Trim(),
                GrantedById = managerEmployeeId,
                CreatedAt = DateTime.UtcNow
            });
            await _uow.SaveChangesAsync();
        }

        public async Task<int> EnsureEntitlementsForYearAsync(int year)
        {
            var policies = await _uow.LeavePolicies.GetAllPoliciesAsync();
            var yearlyPolicies = policies.Where(IsYearlyAllowance).ToList();
            var earned = policies.FirstOrDefault(p => p.LeaveType == LeaveType.Earned);
            var cfCap = earned?.CarryForwardCap ?? 0;

            var employeeIds = await _uow.Employees.GetActiveIdsAsync();
            var current = (await _uow.LeaveEntitlements.GetForYearAsync(year)).Select(e => (e.EmployeeId, e.LeaveType)).ToHashSet();
            var previous = (await _uow.LeaveEntitlements.GetForYearAsync(year - 1)).ToDictionary(e => (e.EmployeeId, e.LeaveType), e => e.TotalDays);
            var previousUsage = (await _uow.LeaveApplications.GetUsageForYearAsync(year - 1)).ToDictionary(u => (u.EmployeeId, u.Type));

            var toAdd = new List<LeaveEntitlement>();
            foreach (var id in employeeIds)
            {
                // Yearly allowances: carry over the employee's own allowance (Admin edits survive), else the policy default.
                foreach (var p in yearlyPolicies)
                {
                    if (current.Contains((id, p.LeaveType))) continue;
                    var total = previous.TryGetValue((id, p.LeaveType), out var prev) ? prev : p.YearlyDays;
                    toAdd.Add(new LeaveEntitlement { EmployeeId = id, Year = year, LeaveType = p.LeaveType, TotalDays = total, CreatedAt = DateTime.UtcNow });
                }

                // Earned CF: unused Earned from last year (+ unused CF), limited to the job-life cap.
                if (cfCap > 0 && !current.Contains((id, LeaveType.EarnedCarryForward)))
                {
                    previous.TryGetValue((id, LeaveType.Earned), out var earnedTotal);
                    previous.TryGetValue((id, LeaveType.EarnedCarryForward), out var cfGranted);
                    previousUsage.TryGetValue((id, LeaveType.Earned), out var u);

                    // Pending counts as used so days are not carried over and then approved a second time.
                    var usedDays = u?.Days ?? 0;
                    var usedFromCf = u?.CarryForwardDays ?? 0;
                    var carry = EarnedCarryForward.Next(cfGranted, usedFromCf, earnedTotal, usedDays - usedFromCf, cfCap);

                    toAdd.Add(new LeaveEntitlement { EmployeeId = id, Year = year, LeaveType = LeaveType.EarnedCarryForward, TotalDays = carry, CreatedAt = DateTime.UtcNow });
                }
            }

            if (toAdd.Count == 0) return 0;
            await _uow.LeaveEntitlements.AddManyAsync(toAdd);
            await _uow.SaveChangesAsync();
            return toAdd.Count;
        }
    }

    public sealed class HolidayService : IHolidayService
    {
        private readonly IHrmUnitOfWork _uow;
        public HolidayService(IHrmUnitOfWork uow) => _uow = uow;

        private static HolidayDto ToDto(Holiday h) => new(h.Id, h.Date, h.Name);

        public async Task<IReadOnlyList<HolidayDto>> GetByYearAsync(int year) =>
            (await _uow.Holidays.GetByYearAsync(year)).Select(ToDto).ToList();

        public async Task<HolidayDto?> GetByIdAsync(int id)
        {
            var h = await _uow.Holidays.GetActiveByIdAsync(id);
            return h is null ? null : ToDto(h);
        }

        public async Task<HolidayDto> CreateAsync(DateOnly date, string name)
        {
            name = ValidateName(name);
            if (await _uow.Holidays.DateExistsAsync(date))
                throw new InvalidOperationException($"A holiday is already defined for {date:dd MMM yyyy}.");

            var holiday = new Holiday { Date = date, Name = name, CreatedAt = DateTime.UtcNow };
            await _uow.Holidays.AddAsync(holiday);
            await _uow.SaveChangesAsync();
            return ToDto(holiday);
        }

        public async Task UpdateAsync(int id, DateOnly date, string name)
        {
            name = ValidateName(name);
            var holiday = await _uow.Holidays.GetActiveByIdAsync(id) ?? throw new InvalidOperationException("Holiday not found.");
            if (await _uow.Holidays.DateExistsAsync(date, id))
                throw new InvalidOperationException($"A holiday is already defined for {date:dd MMM yyyy}.");

            holiday.Date = date;
            holiday.Name = name;
            holiday.UpdatedAt = DateTime.UtcNow;
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var holiday = await _uow.Holidays.GetActiveByIdAsync(id) ?? throw new InvalidOperationException("Holiday not found.");
            holiday.IsDeleted = true; // soft delete, consistent with BaseEntity
            holiday.UpdatedAt = DateTime.UtcNow;
            await _uow.SaveChangesAsync();
        }

        private static string ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Holiday name cannot be empty.", nameof(name));
            return name.Trim();
        }
    }
}
