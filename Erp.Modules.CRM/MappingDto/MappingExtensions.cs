using Erp.Modules.CRM.DTOs;
using Erp.Modules.CRM.Entities;
using Erp.Modules.CRM.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.MappingDto
{
    public static class MappingExtensions
    {
        public static MotherCompanyDto ToDto(this MotherCompany model)
        {
            return new MotherCompanyDto(
                Id: model.Id,
                Name: model.Name,
                Description: model.Description
                );
        }

        public static List<MotherCompanyDto> ToListDto(this IEnumerable<MotherCompany> model)
        {
            return model.Select(x => x.ToDto()).ToList();
        }
        public static CompanyDto ToDto(this Company model)
        {
            return new CompanyDto(
                Id: model.Id,
                Name: model.Name,
                Description: model.Description,
                CompanyEmail: model.CompanyEmail,
                CompanyPhone: model.CompanyPhone,
                CompanyAddress: model.CompanyAddress
                );
        }
        public static List<CompanyDto> ToListDto(this IEnumerable<Company> model)
        {
            return model.Select(x => x.ToDto()).ToList();
        }
        public static CompanyCreateDto ToCreateDto(this CompanyFormViewModel vm)
        {
            return new CompanyCreateDto(
                Name: vm.Name,
                Description: vm.Description,
                MotherCompanyId: vm.MotherCompanyId,
                CompanyEmail: vm.CompanyEmail,
                CompanyPhone: vm.CompanyPhone,
                CompanyAddress: vm.CompanyAddress
                );
        }
    }
}
