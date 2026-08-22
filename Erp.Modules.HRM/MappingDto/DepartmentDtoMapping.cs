using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.MappingDto
{
    public static class DepartmentDtoMapping
    {
        public static DepartmentDto ToDto(this Department model)
        {
            return new()
            {
                Id = model.Id,
                Name = model.Name,
                DepartmentCode = model.DepartmentCode,
                Description = model.Description,
                IsActive = model.IsActive
            };
        }


        public static List<DepartmentDto> ToListDto(this IEnumerable<Department> model)
        {
            return model.Select(x => x.ToDto()).ToList();
        }

        public static DepartmentUpdateDto ToEditDto(this Department model)
        {
            return new()
            {
                Id = model.Id,
                Name = model.Name,
                DepartmentCode = model.DepartmentCode,
                Description = model.Description,
                IsActive = model.IsActive

            };
        }
    }
}
