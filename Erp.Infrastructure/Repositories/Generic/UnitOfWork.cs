using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.HRM;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.Generic
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IDesignationRepository Designations { get; }        

        public IDepartmentRepository Departments {  get; }

        public IEmployeeRepository Employees { get; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Designations = new DesignationRepository(context);
            Departments = new DepartmentRepository(context);
            Employees = new EmployeeRepository(context);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task RollBackDatabase()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
