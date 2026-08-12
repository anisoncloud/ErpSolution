using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.MappingDto;
using Erp.Modules.HRM.Entities;
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

        public async Task<bool> CreateDesignation(DesignationCreateDto dto)
        {
            var isExists = await _uow.Designations.GetByNameAsync(dto.Title);
            if (isExists != null)
            {
                throw new InvalidOperationException(
                   $"A branch with the name'{dto.Title}' is already exists");
            }
            var designation = new Designation
            {
                Title = dto.Title,
            };
            await _uow.Designations.AddAsync(designation);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DesignationDto>> GetDesignationAsync()
        {
            var designations = await _uow.Designations.GetAllAsync();
            return designations.Tode;

        }
    }
}
