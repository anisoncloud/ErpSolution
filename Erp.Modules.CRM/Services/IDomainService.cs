using Erp.Modules.CRM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.Services
{
    public interface IDomainService
    {
        Task<DomainDto> CreateCompany(DomainCreateDto dto);
        Task<IEnumerable<DomainDto>> GetAllAsync();
    }
}
