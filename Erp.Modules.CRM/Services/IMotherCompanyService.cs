using Erp.Modules.CRM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.Services
{
    public interface IMotherCompanyService
    {
        Task<IEnumerable<MotherCompanyDto>> GetMotherCompanyAsync();
        Task<MotherCompanyDto> CreateMotherCompany(MotherCompanyCreateDto dto);
        Task<MotherCompanyDto?> GetMotherCompanyByIdAsync(int id);
        Task<bool> IsNameAvailableAsync(string name, int? excludeId = null);
    }
}
