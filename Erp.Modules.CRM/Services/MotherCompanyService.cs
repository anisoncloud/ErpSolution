using Erp.Modules.CRM.DTOs;
using Erp.Modules.CRM.Entities;
using Erp.Modules.CRM.MappingDto;
using Erp.Modules.CRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.Services
{
    public class MotherCompanyService : IMotherCompanyService
    {
        private readonly ICrmUnitOfWork _uow;
        public MotherCompanyService(ICrmUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> IsNameAvailableAsync(string name, int? excludeId = null)
        {
            bool exists = await _uow.MotherCompanys.ExistsAsync(
                c => c.Name.ToLower() == name.ToLower() &&
                     (!excludeId.HasValue || c.Id != excludeId.Value)
            );

            return !exists;
        }
        public async Task<DTOs.MotherCompanyDto> CreateMotherCompany(MotherCompanyCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Mother Company name can not be empty", nameof(dto.Name));
            }
            var isExists = await _uow.MotherCompanys.ExistsAsync(c=>c.Name == dto.Name);
            if (isExists != null)
            {
                throw new InvalidOperationException(
                   $"A Mother Company with the name {dto.Name.ToUpper()} is already exists!");
            }
            var model = new MotherCompany
            {
                Name = dto.Name.Trim(),
                Description = dto.Description                
            };
            await _uow.MotherCompanys.AddAsync(model);
            await _uow.SaveChangesAsync();
            return model.ToDto();
        }

        public Task<IEnumerable<DTOs.MotherCompanyDto>> GetMotherCompanyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DTOs.MotherCompanyDto?> GetMotherCompanyByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
