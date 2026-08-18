using Erp.Core.Interfaces;
using Erp.Infrastructure.Repositories.HRM;
using Erp.Modules.HRM.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Data
{
    public class UnitOfWork : IHrmUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _transaction;
        public IDesignationRepository Designations { get; }
        public IDepartmentRepository Departments { get; }
        public IEmployeeRepository Employees { get; }
        public ICompanyRepository Companies { get; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Designations = new DesignationRepository(context);
            Departments = new DepartmentRepository(context);
            Employees = new EmployeeRepository(context);
            Companies = new CompanyRepository(context);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {

                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        /// <summary>
        /// Preferred entry point for any multi-repository or cross-module write.
        /// Uses EF Core's execution strategy so this remains correct even if
        /// EnableRetryOnFailure() is later turned on for SqlServer — a raw
        /// BeginTransaction() call would throw under a retrying strategy.
        /// </summary>
        public async Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> operation)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var result = await operation();
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return result;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task ExecuteInTransactionAsync(Func<Task> operation)
        {
            await ExecuteInTransactionAsync(async () =>
            {
                await operation();
                return true; // discarded — this overload just needs the transaction wrapper
            });
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }
}
