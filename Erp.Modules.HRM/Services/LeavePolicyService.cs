using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Enums;
using Erp.Modules.HRM.Repositories;
using Erp.Modules.HRM.StaticData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public class LeavePolicyService : ILeavePolicyService
    {
        private readonly IHrmUnitOfWork _uow;

        public LeavePolicyService(IHrmUnitOfWork uow)
        {
            _uow = uow;
        }

        public static IReadOnlyList<LeavePolicy> Defaults() => new List<LeavePolicy>
        {
            new() { LeaveType = LeaveType.Casual, YearlyDays = 10 },
            new() { LeaveType = LeaveType.Sick, YearlyDays = 14 },
            new() { LeaveType = LeaveType.Earned, YearlyDays = 18, MinDays = 3, CarryForwardCap = 60 },
            new() { LeaveType = LeaveType.Pilgrimage, LifetimeMaxTimes = 1, FixedDays = 30, CountAllCalendarDays = true, SplitByYear = false },
            new() { LeaveType = LeaveType.Maternity, LifetimeMaxTimes = 2, FixedDays = 180, CountAllCalendarDays = true, SplitByYear = false, RequiredGender = Gender.Female },
            new() { LeaveType = LeaveType.Paternity, LifetimeMaxTimes = 2, FixedDays = 7, CountAllCalendarDays = true, SplitByYear = false, RequiredGender = Gender.Male },
            // No automatic allowance: a manager sets the days for an employee.
            new() { LeaveType = LeaveType.Compensation }
        };

        public static LeavePolicyDto ToDto(LeavePolicy p) => new(
            p.LeaveType, p.YearlyDays, p.LifetimeMaxTimes, p.FixedDays, p.MinDays,
            p.CountAllCalendarDays, p.SplitByYear, p.RequiredGender, p.CarryForwardCap);

        public async Task<IReadOnlyList<LeavePolicyDto>> GetAllAsync() =>
            (await _uow.LeavePolicies.GetAllPoliciesAsync()).Where(p => p.LeaveType.IsApplicable()).Select(ToDto).ToList();

        public async Task<LeavePolicyDto?> GetAsync(LeaveType type)
        {
            var p = await _uow.LeavePolicies.GetByTypeAsync(type);
            return p is null ? null : ToDto(p);
        }

        public async Task UpdateAsync(LeavePolicyDto dto)
        {
            if (!dto.LeaveType.IsApplicable())
                throw new ArgumentException("This leave type has no policy of its own; edit the Earned policy instead.");
            if (dto.YearlyDays < 0 || dto.LifetimeMaxTimes < 0 || dto.FixedDays < 0 || dto.MinDays < 0 || dto.CarryForwardCap < 0)
                throw new ArgumentException("Values cannot be negative.");
            if (dto.LifetimeMaxTimes > 0 && dto.FixedDays <= 0)
                throw new ArgumentException("A job-life limit needs Fixed days (the allocation is Fixed days x times).");
            if (dto.FixedDays > 0 && dto.MinDays > dto.FixedDays)
                throw new ArgumentException("Minimum days cannot exceed Fixed days.");
            if (dto.LifetimeMaxTimes > 0 && dto.YearlyDays > 0)
                throw new ArgumentException("A job-life leave cannot also have a yearly allowance.");
            if (dto.CarryForwardCap > 0 && dto.LeaveType != LeaveType.Earned)
                throw new ArgumentException("Only Earned leave can carry forward.");

            var p = await _uow.LeavePolicies.GetByTypeAsync(dto.LeaveType)
                    ?? throw new InvalidOperationException("Policy not found.");

            p.YearlyDays = dto.YearlyDays;
            p.LifetimeMaxTimes = dto.LifetimeMaxTimes;
            p.FixedDays = dto.FixedDays;
            p.MinDays = dto.MinDays;
            p.CountAllCalendarDays = dto.CountAllCalendarDays;
            p.SplitByYear = dto.SplitByYear;
            p.RequiredGender = dto.RequiredGender;
            p.CarryForwardCap = dto.CarryForwardCap;
            p.UpdatedAt = DateTime.UtcNow;
            await _uow.SaveChangesAsync();
        }

        public async Task<int> EnsureDefaultsAsync()
        {
            var existing = (await _uow.LeavePolicies.GetAllPoliciesAsync()).Select(p => p.LeaveType).ToHashSet();
            var missing = Defaults().Where(d => !existing.Contains(d.LeaveType)).ToList();
            if (missing.Count == 0) return 0;
            foreach (var m in missing) m.CreatedAt = DateTime.UtcNow;
            await _uow.LeavePolicies.AddManyAsync(missing);
            await _uow.SaveChangesAsync();
            return missing.Count;
        }
    }
}
