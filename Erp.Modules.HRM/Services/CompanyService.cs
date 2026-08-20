using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.MappingDto;
using Erp.Modules.HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IHrmUnitOfWork _uow;
        public CompanyService(IHrmUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<CompanyDto> CreateCompany(CompanyCreateDto dto)
        {
            if (dto==null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Company name can not be empty", nameof(dto.Name));
            }
            var isExists = await _uow.Companies.GetByNameAsync(dto.Name);
            if (isExists != null)
            {
                throw new InvalidOperationException(
                   $"A Company with the name {dto.Name.ToUpper()} is already exists!");
            }
            var isCodeExists = await _uow.Companies.GetByCodeAsync(dto.CompanyCode);
            if (isCodeExists != null)
            {
                throw new InvalidOperationException(
                   $"A Company Code with the name {dto.CompanyCode.ToUpper()} is already exists!");
            }
            var model = new Company
            {
                Name = dto.Name.Trim(),
                CompanyCode = dto.CompanyCode.Trim(),
                Description = dto.Description.Trim()
            };
            await _uow.Companies.AddAsync(model);
            await _uow.SaveChangesAsync();
            return model.ToDto();
        }

        public async Task<IEnumerable<CompanyDto>> GetCompanyAsync()
        {
            var model = await _uow.Companies.GetAllAsync();
            return model.ToListDto();

        }
        public async Task<CompanyEditDto?> GetCompanyByIdAsync(int id)
        {
            var model = await _uow.Companies.GetByIdAsync(id);
            return model.ToEditDto();
        }
    }
}
