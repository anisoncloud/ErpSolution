using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.Generic
{
    public interface IUnitOfWork : IDisposable
    {
        //IGenericRepository<Designation> Designations { get; }
        IDesignationRepository Designations { get; }
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees { get; }
        Task<int> CommitAsync();
        Task RollBackDatabase();
    }
}
