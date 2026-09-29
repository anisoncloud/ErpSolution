using Erp.Modules.CRM.DTOs;
using Erp.Modules.CRM.MappingDto;
using Erp.Modules.CRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.Services
{
    public class CrmContactService : ICrmContactService
    {
        private readonly ICrmUnitOfWork _uow;
        public CrmContactService(ICrmUnitOfWork uow)
        {
            _uow = uow;
        }
        public Task<CrmContactDto> CreateCompany(CrmContactCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CrmContactDto>> GetAllContactsAsync()
        {          
            var model = await _uow.CrmContacts.GetAllContactWithCompany();
            return model.ToListDto();
        }
    
    }
}
