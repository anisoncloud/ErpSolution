using Erp.Modules.CRM.DTOs;
using Erp.Modules.CRM.Entities;
using Erp.Modules.CRM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net;
using System.Text;
using System.Xml.Linq;

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
        //Company Dto with contact
        public static CompanyDto ToDtoWithContact(this Company model)
        {
            return new CompanyDto(
                Id: model.Id,
                Name: model.Name,
                Description: model.Description,
                CompanyEmail: model.CompanyEmail,
                CompanyPhone: model.CompanyPhone,
                CompanyAddress: model.CompanyAddress,
                Contacts: model.Contacts.Select(x => x.ToDto()).ToList()
                );
        }
        public static List<CompanyDto> ToDtoListWithContacts(this IEnumerable<Company> model)
        {
            return model.Select(c=>c.ToDtoWithContact()).ToList();
                                
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


        public static CrmContactDto ToDto(this Contact model)
        {
            return new CrmContactDto
            {
                Id = model.Id,
                Name = model.Name,
                Designation = model.Designation,
                Phone = model.Phone,
                Email = model.Email,
                Comments = model.Comments,
                Photo = model.Photo,
                CrmCompanyId = model.CrmCompanyId,
                Company = model.Company
            };
            
        }
        public static List<CrmContactDto> ToListDto(this IEnumerable<Contact> model)
        {
            return model.Select(x => x.ToDto()).ToList();
        }

        /// Domain Mapping
        /// 

        public static DomainDto ToDto(this Domains model)
        {
            return new DomainDto
            (
                Id: model.Id,
                Name: model.Name,
                NormalizedName:model.NormalizedName,
                IpAddress:model.IpAddress,
                Hosting:model.Hosting,
                DomainRegistrant: model.DomainRegistrant,
                RegistarDate:model.RegistarDate,
                LastUpdated:model.LastUpdated,
                ForYear:model.ForYear,
                ExpireDate:model.ExpireDate,
                Dns:model.Dns,
                Analytics:model.Analytics,
                CompanyId: model.CompanyId,
                Company: model.Company,
                Comments:model.Comments
            );
        }
        public static List<DomainDto> ToListDto(this IEnumerable<Domains> model)
        {
            return model.Select(x => x.ToDto()).ToList();
        }

        public static DomainCreateDto ToCreateDto(this DomainFormViewModel model)
        {
            return new DomainCreateDto
            (
                Name: model.Name,
                NormalizedName: model.NormalizedName,
                IpAddress: model.IpAddress,
                Hosting: model.Hosting,
                DomainRegistrant: model.DomainRegistrant,
                RegistarDate: model.RegistarDate,
                LastUpdated: model.LastUpdated,
                ForYear: model.ForYear,
                ExpireDate: model.ExpireDate,
                Dns: model.Dns,
                Analytics: model.Analytics,
                CompanyId: model.CompanyId,
                Company: model.Company,
                Comments: model.Comments
            );
        }
        
    }
}
