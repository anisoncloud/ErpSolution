using Erp.Modules.HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.DataStore.DataUnit
{
    public interface IUnitOfWork : IDisposable
    {
        //IGenericRepository<Designation> Designations { get; }
        IDesignationRepository Designations { get; }
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        /// <summary>
        /// Runs the given operation inside a transaction and commits on success,
        /// rolls back on any exception. Handles EF Core's retry-on-failure execution
        /// strategy correctly (required when SqlServer connection resiliency is enabled).
        /// Use this for anything that touches more than one repository/module.
        /// </summary>
        Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> operation);
        Task ExecuteInTransactionAsync(Func<Task> operation);
    }
}
