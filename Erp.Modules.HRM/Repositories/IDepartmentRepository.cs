using Erp.Core.Interfaces;
using Erp.Modules.HRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<Department?> GetCompanyWithAllEmployee(int id);
        Task<Department?> GetByIdAsync(int id);
        Task<Department?> GetByNameAsync(string name);
        Task<Department?> GetByCodeAsync(string code);
    }
}
