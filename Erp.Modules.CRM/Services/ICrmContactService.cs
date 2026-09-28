using Erp.Modules.CRM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.Services
{
    public interface ICrmContactService
    {
        Task<CrmContactDto> CreateCompany(CrmContactCreateDto dto);
        Task<IEnumerable<CrmContactDto>> GetAllContactsAsync();
    }
}
