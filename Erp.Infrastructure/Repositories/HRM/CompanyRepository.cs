using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.HRM
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }

        public Task<Company?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Company?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Company?> GetCompanyWithAllEmployee(int id)
        {
            throw new NotImplementedException();
        }
    }
}
