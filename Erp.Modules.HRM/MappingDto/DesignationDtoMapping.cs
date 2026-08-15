using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.MappingDto
{
    public static class DesignationDtoMapping
    {
        public static DesignationDto ToDesignationDto(this Designation model)
        {
            return new()
            {
                Id = model.Id,
                PublicId = model.PublicId,
                Title = model.Title,
            };
        }


        public static List<DesignationDto> ToListDesignationDto(this IEnumerable<Designation> model)
        {
            return model.Select(x=>x.ToDesignationDto()).ToList();
        }
    }
}
