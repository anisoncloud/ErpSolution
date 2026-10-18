using Erp.Modules.HRM.Enums;
using Erp.Modules.HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public sealed record LeavePolicyDto(
    LeaveType LeaveType,
    int YearlyDays,
    int LifetimeMaxTimes,
    int FixedDays,
    int MinDays,
    bool CountAllCalendarDays,
    bool SplitByYear,
    Gender? RequiredGender,
    int CarryForwardCap);

    /// <param name="Scope">"2026" for yearly types, "Job life" for lifetime types.</param>
    /// <param name="FromCarryForward">Earned only: how many of this year's days will be taken from the Earned CF balance.</param>
    public sealed record YearBalance(int Year, string Scope, int Total, int Used, int Pending, int RemainingBefore, int RemainingAfter, int FromCarryForward);

    public sealed record LeavePreview(
        LeaveDayResult? Result,
        DateOnly? ReturnDate,
        IReadOnlyList<YearBalance> Balances,
        IReadOnlyList<string> Errors)
    {
        public bool IsValid => Errors.Count == 0 && Result is { TotalDays: > 0 };
    }

    public sealed record SubmitResult(LeavePreview Preview, int? ApplicationId);

    public sealed record CarryForwardBalance(int Allocated, int Used, int Pending)
    {
        public int Remaining => Allocated - Used - Pending;
    }

    /// <summary>One report row. Yearly types: this year's allocation. Lifetime types: job-life allocation (days x times).</summary>
    public sealed record LeaveTypeBalance(
        LeaveType Type, string Scope, int Allocated, int Used, int Pending, CarryForwardBalance? CarryForward = null)
    {
        public int Remaining => Allocated - Used - Pending;
    }

    public sealed record LeaveReportItem(
        int Id, LeaveType Type, DateOnly From, DateOnly Return, int TotalDays, int DaysInYear,
        LeaveStatus Status, string? ManagerComment, string? DecidedByName, DateTime? DecidedAt);

    public sealed record LeaveReport(int Year, IReadOnlyList<LeaveTypeBalance> Balances, IReadOnlyList<LeaveReportItem> Applications);

    public sealed record PendingLeaveItem(int Id, string EmployeeName, LeaveType Type, DateOnly From, DateOnly Return, int Days);

    public sealed record DecideResult(int Processed, IReadOnlyList<string> Errors);
    public interface ILeaveService
    {
        Task<EmployeeRef?> GetEmployeeByUserAsync(Guid userId);
        Task<EmployeeRef?> GetEmployeeAsync(int employeeId);
        Task<IReadOnlyList<EmployeeRef>> GetEmployeesAsync();
        Task<IReadOnlyList<EmployeeRef>> GetTeamAsync(int managerEmployeeId);

        /// <summary>Leave types this employee can apply for (gender rules applied).</summary>
        Task<IReadOnlyList<LeavePolicyDto>> GetApplicableTypesAsync(int employeeId);

        /// <summary>Validates and calculates without saving (confirmation page). For fixed-day leave the return date
        /// is calculated and <paramref name="returnDate"/> is ignored.</summary>
        Task<LeavePreview> PreviewAsync(int employeeId, LeaveType type, DateOnly from, DateOnly? returnDate);

        /// <summary>Re-validates and saves a Pending application. Never trusts an earlier preview.</summary>
        Task<SubmitResult> SubmitAsync(int employeeId, LeaveType type, DateOnly from, DateOnly? returnDate);

        Task<LeaveReport> GetReportAsync(int employeeId, int year);
        Task<IReadOnlyList<PendingLeaveItem>> GetPendingForManagerAsync(int managerEmployeeId);
        Task<DecideResult> DecideAsync(int managerEmployeeId, IEnumerable<int> applicationIds, bool approve, string? comment);

        /// <summary>Admin: effective yearly totals for Casual, Sick, Earned, EarnedCarryForward, Compensation
        /// (existing row, otherwise the policy default).</summary>
        Task<Dictionary<LeaveType, int>> GetEntitlementTotalsAsync(int employeeId, int year);

        /// <summary>Admin: sets the given totals; types not supplied keep their row or get the policy default.
        /// Earned CF cannot exceed the policy cap.</summary>
        Task AssignEntitlementsAsync(int employeeId, int year, IReadOnlyDictionary<LeaveType, int>? totals = null);

        /// <summary>Manager: sets extra Compensation leave days for one of their team members.</summary>
        Task GrantCompensationAsync(int managerEmployeeId, int employeeId, int year, int days, string reason);

        /// <summary>Idempotent: creates missing yearly entitlements and Earned CF for every active employee.</summary>
        Task<int> EnsureEntitlementsForYearAsync(int year);
    }

    public interface ILeavePolicyService
    {
        Task<IReadOnlyList<LeavePolicyDto>> GetAllAsync();
        Task<LeavePolicyDto?> GetAsync(LeaveType type);

        /// <summary>Throws ArgumentException with a readable message when the combination of values is invalid.</summary>
        Task UpdateAsync(LeavePolicyDto dto);

        /// <summary>Seeds the default policies if missing. Returns rows added.</summary>
        Task<int> EnsureDefaultsAsync();
    }

    public sealed record HolidayDto(int Id, DateOnly Date, string Name);

    public interface IHolidayService
    {
        Task<IReadOnlyList<HolidayDto>> GetByYearAsync(int year);
        Task<HolidayDto?> GetByIdAsync(int id);
        Task<HolidayDto> CreateAsync(DateOnly date, string name);
        Task UpdateAsync(int id, DateOnly date, string name);
        Task DeleteAsync(int id);
    }
}
