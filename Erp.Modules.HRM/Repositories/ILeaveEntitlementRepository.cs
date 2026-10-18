using Erp.Core.Interfaces;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    public interface ILeaveEntitlementRepository : IGenericRepository<LeaveEntitlement>
    {
        Task<LeaveEntitlement?> GetAsync(int employeeId, int year, LeaveType type);
        Task<List<LeaveEntitlement>> GetForEmployeeAsync(int employeeId, int year);
        Task<List<LeaveEntitlement>> GetForYearAsync(int year);
        Task AddManyAsync(IEnumerable<LeaveEntitlement> items);
    }
}
