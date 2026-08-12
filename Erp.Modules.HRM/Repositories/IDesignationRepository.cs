using Erp.Core.Interfaces;
using Erp.Modules.HRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    public interface IDesignationRepository : IGenericRepository<Designation>
    {
        Task<Designation?> GetDesignationWithAllEmployee(Guid id);
        Task<Designation?> GetByIdAsync(Guid id);
        Task<Designation?> GetByNameAsync(string name);
    }
}
