using Erp.Core.Pagination;
using Erp.Modules.CRM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.Services
{
    public interface ICrmCompanyService
    {
        Task<CompanyDto> CreateCompany(CompanyCreateDto dto);
        Task<IEnumerable<CompanyDto>> GetAllCompanys();
        Task<PagedResult<CompanyListItemDto>> GetAllCompanyListAsync(GridQueryParameters parameters);
    }
}
