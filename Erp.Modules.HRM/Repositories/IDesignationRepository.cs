using Erp.Modules.HRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    public interface IDesignationRepository
    {
        Task<Designation?> GetDesignationWithAllEmployee(Guid id);
    }
}
