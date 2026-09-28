using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.CRM.Entities;
using Erp.Modules.CRM.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.CRM
{
    public class CrmContactRepository : GenericRepository<Contact>, ICrmContactRepository
    {
        public CrmContactRepository(AppDbContext context) : base(context)
        {
            
        }
        public async Task<IEnumerable<Contact>> GetAllContactWithCompany()
        {
            return await _dbSet.Include(x=>x.Company).ToListAsync();
        }
    }
}
