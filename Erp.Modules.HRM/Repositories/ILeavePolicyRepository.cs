using Erp.Core.Interfaces;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    public interface ILeavePolicyRepository : IGenericRepository<LeavePolicy>
    {
        Task<List<LeavePolicy>> GetAllPoliciesAsync();
        Task<LeavePolicy?> GetByTypeAsync(LeaveType type);
        Task AddManyAsync(IEnumerable<LeavePolicy> items);
    }
}
