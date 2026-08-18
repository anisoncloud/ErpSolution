using Erp.Core.Interfaces;
using Erp.Modules.HRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<Company?> GetCompanyWithAllEmployee(int id);
        Task<Company?> GetByIdAsync(int id);
        Task<Company?> GetByNameAsync(string name);
    }
}
