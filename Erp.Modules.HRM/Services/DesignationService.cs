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

        public async Task<DesignationDto> CreateDesignation(DesignationCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                throw new ArgumentException("Designation name can not be empty", nameof(dto.Title));
            }
            var isExists = await _uow.Designations.GetByNameAsync(dto.Title);
            if (isExists != null)
            {
                throw new InvalidOperationException(
                   $"A Company with the name {dto.Title.ToUpper()} is already exists!");
            }
            var isCodeExists = await _uow.Designations.GetByCodeAsync(dto.DesignationCode);
            if (isCodeExists != null)
            {
                throw new InvalidOperationException(
                   $"A Company Code with the name {dto.DesignationCode.ToUpper()} is already exists!");
            }
            var model = new Designation
            {
                Title = dto.Title.Trim(),
                DesignationCode = dto.DesignationCode,
                IsActive = dto.IsActive,
            };
            await _uow.Designations.AddAsync(model);
            await _uow.SaveChangesAsync();
            return model.ToDto();
        }

        public async Task<IEnumerable<DesignationDto>> GetDesignationAsync()
        {
            var model = await _uow.Designations.GetAllAsync();
            return model.ToListDto();

        }
        public async Task<IEnumerable<DesignationDto>> GetDesignationAscSortNameAsync()
        {
            var model = await _uow.Designations.GetAllAsync(c => c.OrderBy(d => d.Title));
            return model.ToListDto();
        }
        public async Task<DesignationDto?> GetDesignationByIdAsync(int id)
        {
            var model = await _uow.Designations.GetByIdAsync(id);
            return model.ToDto();
        }

        public async Task<DesignationDto> UpdateDesignationAsync(int id, DesignationUpdateDto dto)
        {
            var designation = await _uow.Designations.GetByIdAsync(id);
            designation.Title = dto.Title.Trim();
            designation.DesignationCode = dto.DesignationCode;
            designation.IsActive = dto.IsActive;

            await _uow.Designations.UpdateAsync(designation);
            await _uow.SaveChangesAsync();
            return designation.ToDto();
        }
    }
}
