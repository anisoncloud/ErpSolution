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
                Title = model.Title,
            };
        }
    }
}
