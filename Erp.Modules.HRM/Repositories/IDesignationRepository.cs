using Erp.Modules.HRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    public interface IDesignationRepository
    {
        Task<IReadOnlyList<Designation>> GetAllAsync();
        Task<Designation?> GetByIdAsync(Guid id);
        Task<Designation?> GetDesignationWithAllEmployee(Guid id);
    }
}
