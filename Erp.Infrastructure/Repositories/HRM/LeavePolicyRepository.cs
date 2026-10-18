using Microsoft.EntityFrameworkCore;
using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Enums;
using Erp.Modules.HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.HRM
{
    public class LeavePolicyRepository : GenericRepository<LeavePolicy>, ILeavePolicyRepository
    {
        public LeavePolicyRepository(AppDbContext context) : base(context)
        {
        }

        public Task<List<LeavePolicy>> GetAllPoliciesAsync() =>
        _dbSet.Where(p => !p.IsDeleted).OrderBy(p => p.LeaveType).ToListAsync();

        public Task<LeavePolicy?> GetByTypeAsync(LeaveType type) =>
            _dbSet.FirstOrDefaultAsync(p => p.LeaveType == type && !p.IsDeleted);

        public Task AddManyAsync(IEnumerable<LeavePolicy> items) => _dbSet.AddRangeAsync(items);
    }
}
