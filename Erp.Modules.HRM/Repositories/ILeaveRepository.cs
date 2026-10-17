using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    public sealed record EmployeeRef(int Id, string FullName, int? ManagerId, Gender Gender);

    /// <summary>Pending + Approved days for one leave type in one year. Cf = days taken from the Earned CF balance.</summary>
    public sealed record UsageRow(LeaveType Type, int ApprovedDays, int PendingDays, int ApprovedCf, int PendingCf);

    public sealed record LifetimeUsage(int ApprovedTimes, int PendingTimes, int ApprovedDays, int PendingDays);

    public sealed record EmployeeYearUsage(int EmployeeId, LeaveType Type, int Days, int CarryForwardDays);
    public interface ILeaveRepository
    {
        // employees
        Task<EmployeeRef?> GetEmployeeByUserIdAsync(Guid userId);
        Task<EmployeeRef?> GetEmployeeAsync(int employeeId);
        Task<List<int>> GetActiveEmployeeIdsAsync();
        Task<List<EmployeeRef>> GetEmployeesAsync();
        Task<List<EmployeeRef>> GetTeamAsync(int managerEmployeeId);

        // policies
        Task<List<LeavePolicy>> GetPoliciesAsync();
        Task<LeavePolicy?> GetPolicyAsync(LeaveType type);
        Task AddPoliciesAsync(IEnumerable<LeavePolicy> items);

        // entitlements
        Task<LeaveEntitlement?> GetEntitlementAsync(int employeeId, int year, LeaveType type);
        Task<List<LeaveEntitlement>> GetEntitlementsAsync(int employeeId, int year);
        Task<List<LeaveEntitlement>> GetEntitlementsForYearAsync(int year);
        Task AddEntitlementsAsync(IEnumerable<LeaveEntitlement> items);

        // usage (calculated from applications)
        Task<List<UsageRow>> GetUsageAsync(int employeeId, int year);
        Task<LifetimeUsage> GetLifetimeUsageAsync(int employeeId, LeaveType type);
        Task<List<EmployeeYearUsage>> GetUsageForYearAsync(int year);

        // compensation
        Task AddGrantAsync(CompensationGrant grant);

        // applications
        /// <summary>True when [from, lastDay] overlaps a Pending or Approved application.</summary>
        Task<bool> HasOverlapAsync(int employeeId, DateOnly from, DateOnly lastDay);
        Task AddApplicationAsync(LeaveApplication application);
        Task<List<LeaveApplication>> GetApplicationsForYearAsync(int employeeId, int year);
        Task<List<LeaveApplication>> GetPendingForManagerAsync(int managerEmployeeId);
        Task<List<LeaveApplication>> GetApplicationsByIdsAsync(IEnumerable<int> ids);
    }
}
