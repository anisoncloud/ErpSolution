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

        public async Task<bool> CreateCompany(CompanyCreateDto dto)
        {
            var isExists = await _uow.Companies.GetByNameAsync(dto.Name);
            if (isExists != null)
            {
                throw new InvalidOperationException(
                   $"A branch with the name'{dto.Name}' is already exists");
            }
            var model = new Company
            {
                Name = dto.Name,
            };
            await _uow.Companies.AddAsync(model);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CompanyDto>> GetCompanyAsync()
        {
            var model = await _uow.Companies.GetAllAsync();
            return model.ToListDto();

        }
    }
}
