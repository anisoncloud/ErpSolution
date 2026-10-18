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

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<Employee?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(x=> x.Department)
                .Include (x=> x.Designation)
                .Include(x=>x.Company)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Employee?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(x => x.Department)
                .Include(x => x.Designation)
                .Include(x => x.Company)
                .FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<IReadOnlyList<Employee>> GetByManagerIdAsync(int managerId)
        {
            return await _context.Employees
                         .Where(e => e.EmpManagerId == managerId)
                         .ToListAsync();
        }

        public async Task<Employee?> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet.FirstAsync(x=>x.UserId == userId);
        }

        /*Task<Employee> IEmployeeRepository.AddAsync(Employee entity)
        {
            throw new NotImplementedException();
        }*/


        // Leave Management
        public async Task<EmployeeRef?> GetRefByUserIdAsync(Guid userId)
        {
            var e = await _dbSet.Where(x => x.UserId == userId && !x.IsDeleted)
                .Select(x => new { x.Id, x.FullName, x.EmpManagerId, x.Gender }).FirstOrDefaultAsync();
            return e is null ? null : new EmployeeRef(e.Id, e.FullName, e.EmpManagerId, e.Gender);
        }

        public async Task<EmployeeRef?> GetRefAsync(int employeeId)
        {
            var e = await _dbSet.Where(x => x.Id == employeeId && !x.IsDeleted)
                .Select(x => new { x.Id, x.FullName, x.EmpManagerId, x.Gender }).FirstOrDefaultAsync();
            return e is null ? null : new EmployeeRef(e.Id, e.FullName, e.EmpManagerId, e.Gender);
        }

        public Task<List<int>> GetActiveIdsAsync() =>
            _dbSet.Where(e => e.IsActive && !e.IsDeleted).Select(e => e.Id).ToListAsync();

        public async Task<List<EmployeeRef>> GetRefsAsync()
        {
            var rows = await _dbSet.Where(x => !x.IsDeleted).OrderBy(x => x.FullName)
                .Select(x => new { x.Id, x.FullName, x.EmpManagerId, x.Gender }).ToListAsync();
            return rows.Select(e => new EmployeeRef(e.Id, e.FullName, e.EmpManagerId, e.Gender)).ToList();
        }

        public async Task<List<EmployeeRef>> GetTeamRefsAsync(int managerEmployeeId)
        {
            var rows = await _dbSet.Where(x => !x.IsDeleted && x.IsActive && x.EmpManagerId == managerEmployeeId)
                .OrderBy(x => x.FullName)
                .Select(x => new { x.Id, x.FullName, x.EmpManagerId, x.Gender }).ToListAsync();
            return rows.Select(e => new EmployeeRef(e.Id, e.FullName, e.EmpManagerId, e.Gender)).ToList();
        }
        // End Leave management

    }
}
