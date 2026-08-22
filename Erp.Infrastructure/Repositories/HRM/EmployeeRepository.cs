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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context) { }

        public async Task<bool> EmployeeCodeExistsAsync(string code)
        {
            return await _dbSet.AnyAsync(x=>x.EmployeeCode == code);
        }

        public async Task<IReadOnlyList<Employee>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .Include(x => x.Department)
                .Include(x => x.Designation)
                .OrderBy(x=>x.FullName)
                .ToListAsync();
        }

        public Task<Employee?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(x=> x.Department)
                .Include (x=> x.Designation)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public Task<Employee?> GetByIdWithDetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Employee>> GetByManagerIdAsync(Guid managerId)
        {
            return await Task.FromResult(new List<Employee>());
        }

        public async Task<Employee?> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet.FirstAsync(x=>x.UserId == userId);
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
