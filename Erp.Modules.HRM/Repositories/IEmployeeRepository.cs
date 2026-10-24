using Erp.Core.Interfaces;
using Erp.Modules.HRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee?> GetByIdAsync(Guid id);
        Task<Employee?> GetByIdWithDetailsAsync(Guid id);          // includes Department/Designation
        Task<Employee?> GetByUserIdAsync(Guid userId);             // "my own record" lookup
        Task<IReadOnlyList<Employee>> GetAllWithDetailsAsync();
        Task<IReadOnlyList<Employee>> GetByManagerIdAsync(int managerId); // future: team view
        Task<bool> EmployeeCodeExistsAsync(string code);
        //Task<Employee> AddAsync(Employee entity);
        Task<Employee?> GetActiveByIdAsync(int id);
        Task<List<Employee>> GetDirectReportsAsync(int managerId);

        // Leave management
        Task<EmployeeRef?> GetRefByUserIdAsync(Guid userId);
        Task<EmployeeRef?> GetRefAsync(int employeeId);
        Task<List<int>> GetActiveIdsAsync();
        Task<List<EmployeeRef>> GetRefsAsync();
        Task<List<EmployeeRef>> GetTeamRefsAsync(int managerEmployeeId);
        Task<bool> ExistsActiveAsync(int id);
        Task<List<Employee>> GetActiveByDepartmentAsync(int departmentId);
    }
}
