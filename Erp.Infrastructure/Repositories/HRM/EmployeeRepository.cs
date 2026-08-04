using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.HRM
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context) { }

        public Task<bool> EmployeeCodeExistsAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Employee>> GetAllWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee?> GetByIdWithDetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Employee>> GetByManagerIdAsync(Guid managerId)
        {
            throw new NotImplementedException();
        }

        public Task<Employee?> GetByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
