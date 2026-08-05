using Erp.Modules.HRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IReadOnlyList<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(Guid id);
        Task<Department?> GetDepartmentWithAllEmployye(Guid id);
    }
}
