using Erp.Modules.HRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetByIdAsync(int id);
        Task<Department?> GetDepartmentWithAllEmployye(int id);
    }
}
