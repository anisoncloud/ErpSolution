using Erp.Modules.HRM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public interface ICompanyService 
    {
        Task<IEnumerable<CompanyDto>> GetCompanyAsync();
        Task<CompanyDto> CreateCompany(CompanyCreateDto dto);
    }
}
