using Erp.Core.Interfaces;
using Erp.Core.Pagination;
using Erp.Modules.CRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.Repositories
{
    public interface ICrmCompanyRepository : IGenericRepository<Company>
    {
        Task<PagedResult<Company>> GetAllCompaniesPagedAsync(GridQueryParameters parameters);
    }
}
