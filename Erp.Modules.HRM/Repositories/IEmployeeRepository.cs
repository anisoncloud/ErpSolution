using Erp.Modules.HRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(Guid id);
        Task<Employee?> GetByIdWithDetailsAsync(Guid id);          // includes Department/Designation
        Task<Employee?> GetByUserIdAsync(Guid userId);             // "my own record" lookup
        Task<IReadOnlyList<Employee>> GetAllWithDetailsAsync();
        Task<IReadOnlyList<Employee>> GetByManagerIdAsync(Guid managerId); // future: team view
        Task<bool> EmployeeCodeExistsAsync(string code);
        Task AddAsync(Employee employee);
        void Update(Employee employee);
        Task<int> SaveChangesAsync();
    }
}
