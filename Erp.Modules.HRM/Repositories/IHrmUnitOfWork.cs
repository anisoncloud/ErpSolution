using Erp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    /// <summary>
    /// HRM-specific extension of the base transactional contract.
    /// Lives inside the HRM module itself — HRM is allowed to reference
    /// Erp.Core (Core has no back-reference to HRM), so this is not circular.
    /// </summary>
    public interface IHrmUnitOfWork : IUnitOfWork
    {
        IEmployeeRepository Employees { get; }
        IDepartmentRepository Departments { get; }
        IDesignationRepository Designations { get; }
    }
}
