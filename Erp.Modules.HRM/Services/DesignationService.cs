using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public class DesignationService : IDesignationService
    {
        private readonly IHrmUnitOfWork _uow;
        public DesignationService(IHrmUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<DesignationDto> CreateDesignation(DesignationCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<DesignationDto> GetDesignationAsync()
        {
            throw new NotImplementedException();
        }
    }
}
