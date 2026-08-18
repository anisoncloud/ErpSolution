using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.MappingDto
{
    public static class CompanyDtoMapping
    {
        public static CompanyDto ToDto(this Company model)
        {
            return new()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
            };
        }


        public static List<CompanyDto> ToListDto(this IEnumerable<Company> model)
        {
            return model.Select(x => x.ToDto()).ToList();
        }
    }
}
