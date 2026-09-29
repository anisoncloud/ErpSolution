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
    public class DomainService : IDomainService
    {
        private readonly ICrmUnitOfWork _uow;
        public DomainService(ICrmUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<DomainDto> CreateCompany(DomainCreateDto dto)
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
            var isExists = await _uow.Domains.ExistsAsync(c => c.NormalizedName == NormalizedName);
            if (isExists == true)
            {
                throw new InvalidOperationException(
                   $"A Company with the name {dto.Name.ToUpper()} is already exists!");
            }
            var model = new Domains
            {
                Name = dto.Name.Trim(),
                NormalizedName = NormalizedName,
                IpAddress = dto.IpAddress,
                Hosting = dto.Hosting,
                DomainRegistrant = dto.DomainRegistrant,
                RegistarDate = dto.RegistarDate,
                LastUpdated = dto.LastUpdated,
                ForYear = dto.ForYear,
                ExpireDate = dto.ExpireDate,
                Dns = dto.Dns,
                Analytics = dto.Analytics,
                CompanyId = dto.CompanyId,
                Comments = dto.Comments
            };
            await _uow.Domains.AddAsync(model);
            await _uow.SaveChangesAsync();
            return model.ToDto();
        }
    }
}
