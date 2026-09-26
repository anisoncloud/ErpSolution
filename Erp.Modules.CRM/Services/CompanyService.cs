using Erp.Core.Interfaces;
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
    public class CrmCompanyService : ICrmCompanyService
    {
        private readonly ICrmUnitOfWork _uow;
        public CrmCompanyService(ICrmUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<CompanyDto> CreateCompany(CompanyCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Company name can not be empty", nameof(dto.Name));
            }
            string NormalizedName = NameNormalizer.Normalize(dto.Name);
            var isExists = await _uow.CrmCompanies.ExistsAsync(c => c.NormalizedName == NormalizedName);
            if (isExists == true)
            {
                throw new InvalidOperationException(
                   $"A Company with the name {dto.Name.ToUpper()} is already exists!");
            }
            var model = new Company
            {
                Name = dto.Name.Trim(),
                NormalizedName = NormalizedName,
                Description = dto.Description,
                CompanyAddress = dto.CompanyAddress,
                CompanyPhone = dto.CompanyPhone,
                CompanyEmail = dto.CompanyEmail,
                MotherCompanyId = dto.MotherCompanyId                
            };
            await _uow.CrmCompanies.AddAsync(model);
            await _uow.SaveChangesAsync();
            return model.ToDto();
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompanys()
        {
            var model = await _uow.CrmCompanies.GetAllAsync();
            return model.ToListDto();
        }
    }
}
