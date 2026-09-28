using Erp.Core.Pagination;
using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.CRM.Entities;
using Erp.Modules.CRM.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Erp.Infrastructure.Repositories.CRM
{
    public class CrmCompanyRepository : GenericRepository<Company>, ICrmCompanyRepository
    {
        public CrmCompanyRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<PagedResult<Company>> GetAllCompaniesPagedAsync(GridQueryParameters parameters)
        {
            Expression<Func<Company, bool>>? searchPredicate = null;

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                var term = parameters.SearchTerm.Trim().ToLower();
                searchPredicate = e =>
                    e.Name.ToLower().Contains(term) ||
                    e.CompanyPhone.ToLower().Contains(term) ||
                    (e.CompanyPhone != null && e.CompanyPhone.ToLower().Contains(term));
            }
            return await GetAllItemsAsync(parameters, searchPredicate);
        }

        public async Task<IEnumerable<Company>> GetCompaniesWithContactsAsync()
        {
            var companies = await _dbSet.Include(x=>x.Contacts).ToListAsync();
            return companies;
        }
    }
}
