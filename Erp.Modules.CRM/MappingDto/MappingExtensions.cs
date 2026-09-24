using Erp.Modules.CRM.DTOs;
using Erp.Modules.CRM.Entities;
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

        public static List<MotherCompanyDto> ToListMaintenanceDto(this IEnumerable<MotherCompany> model)
        {
            return model.Select(x => x.ToDto()).ToList();
        }
    }
}
