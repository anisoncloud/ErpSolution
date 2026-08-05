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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Department?> GetDepartmentWithAllEmployye(Guid id)
        {
            return await _dbSet
                .Include(x=>x.Employees)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
