using Erp.Core.Interfaces;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    public interface ILeaveApplicationRepository : IGenericRepository<LeaveApplication>
    {
        /// <summary>True when [from, lastDay] overlaps a Pending or Approved application.</summary>
        Task<bool> HasOverlapAsync(int employeeId, DateOnly from, DateOnly lastDay);

        // usage is calculated from applications, never stored
        Task<List<UsageRow>> GetUsageAsync(int employeeId, int year);
        Task<LifetimeUsage> GetLifetimeUsageAsync(int employeeId, LeaveType type);
        Task<List<EmployeeYearUsage>> GetUsageForYearAsync(int year);

        Task<List<LeaveApplication>> GetApplicationsForYearAsync(int employeeId, int year);
        Task<List<LeaveApplication>> GetPendingForManagerAsync(int managerEmployeeId);
        Task<List<LeaveApplication>> GetApplicationsByIdsAsync(IEnumerable<int> ids);
    }
}
