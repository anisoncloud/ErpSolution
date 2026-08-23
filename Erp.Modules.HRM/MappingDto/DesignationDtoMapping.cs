using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.MappingDto
{
    public static class DesignationDtoMapping
    {
        public static DesignationDto ToDto(this Designation model)
        {
            return new()
            {
                Id = model.Id,
                PublicId = model.PublicId,
                Title = model.Title,
                DesignationCode = model.DesignationCode,
                IsActive = model.IsActive,
            };
        }


        public static List<DesignationDto> ToListDto(this IEnumerable<Designation> model)
        {
            return model.Select(x=>x.ToDto()).ToList();
        }
    }
}
