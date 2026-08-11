using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.HRM
{
    public class DesignationRepository : GenericRepository<Designation>, IDesignationRepository
    {
        public DesignationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Designation?> GetDesignationWithAllEmployee(Guid id)
        {
            return await _dbSet.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Designation?> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            return await _dbSet
                .FirstOrDefaultAsync(b => b.Title.ToLower() == name.ToLower());
        }
    }
}
