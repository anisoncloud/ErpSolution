using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Enums;
using Erp.Modules.HRM.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.HRM
{
    public class LeaveEntitlementRepository : GenericRepository<LeaveEntitlement>, ILeaveEntitlementRepository
    {
        public LeaveEntitlementRepository(AppDbContext context) : base(context)
        {
        }

        public Task<LeaveEntitlement?> GetAsync(int employeeId, int year, LeaveType type)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.Year == year && x.LeaveType == type);
        }

        // tracked: the Admin edit and the compensation grant change these rows and save through the unit of work
        public Task<List<LeaveEntitlement>> GetForEmployeeAsync(int employeeId, int year) =>
            _dbSet.Where(x => x.EmployeeId == employeeId && x.Year == year).ToListAsync();

        public Task<List<LeaveEntitlement>> GetForYearAsync(int year) =>
            _dbSet.AsNoTracking().Where(x => x.Year == year).ToListAsync();

        public Task AddManyAsync(IEnumerable<LeaveEntitlement> items) => _dbSet.AddRangeAsync(items);
    }
}
