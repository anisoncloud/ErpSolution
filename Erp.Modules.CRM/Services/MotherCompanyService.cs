using Erp.Core.StaticClasses;
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
        public async Task<MotherCompanyDto> CreateMotherCompany(MotherCompanyCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Mother Company name can not be empty", nameof(dto.Name));
            }
            string NormalizedName = NameNormalizer.Normalize(dto.Name);
            var isExists = await _uow.MotherCompanys.ExistsAsync(c=>c.NormalizedName == NormalizedName);
            if (isExists == true)
            {
                throw new InvalidOperationException(
                   $"A Mother Company with the name {dto.Name.ToUpper()} is already exists!");
            }
            var model = new MotherCompany
            {
                Name = dto.Name.Trim(),
                NormalizedName = NormalizedName,
                Description = dto.Description                
            };
            await _uow.MotherCompanys.AddAsync(model);
            await _uow.SaveChangesAsync();
            return model.ToDto();
        }

        public async Task<IEnumerable<MotherCompanyDto>> GetMotherCompanyAsync()
        {
            var model = await _uow.MotherCompanys.GetAllAsync();
            return model.ToListDto();
        }

        public async Task<MotherCompanyDto?> GetMotherCompanyByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
