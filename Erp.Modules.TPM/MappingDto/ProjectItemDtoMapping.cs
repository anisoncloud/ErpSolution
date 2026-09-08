using Erp.Modules.TPM.DTOs;
using Erp.Modules.TPM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.MappingDto
{
    public static class ProjectItemDtoMapping
    {
        public static ProjectItemDto ToDto(this ProjectItem model)
        {
            return new()
            {
                Name = model.Name.Trim(),
                DemoStartDate = model.DemoStartDate,
                WorkOrderDate = model.WorkOrderDate,
                ProjectDays = model.ProjectDays,
                ProjectDeliveryDate = model.ProjectDeliveryDate,
                ProjectValue = model.ProjectValue,
                Advanced = model.Advanced,
                ProjectDetails = model.ProjectDetails,
                Proposal = model.Proposal,
                WorkOrder = model.WorkOrder,
                SoftwareRequirement = model.SoftwareRequirement
            };
        }


        public static List<ProjectItemDto> ToListDto(this IEnumerable<ProjectItem> model)
        {
            return model.Select(x => x.ToDto()).ToList();
        }

        public static ProjectItemUpdateDto ToEditDto(this ProjectItem model)
        {
            return new()
            {
                DemoStartDate = model.DemoStartDate,
                WorkOrderDate = model.WorkOrderDate,
                ProjectDays = model.ProjectDays,
                ProjectDeliveryDate = model.ProjectDeliveryDate,
                ProjectValue = model.ProjectValue,
                Advanced = model.Advanced,
                ProjectDetails = model.ProjectDetails,
            };
        }
        public static ProjectItemCreateDto ToCreateDto(this ProjectItem model)
        {
            return new()
            {
                Name = model.Name.Trim(),
                DemoStartDate = model.DemoStartDate,
                WorkOrderDate = model.WorkOrderDate,
                ProjectDays = model.ProjectDays,
                ProjectDeliveryDate = model.ProjectDeliveryDate,
                ProjectValue = model.ProjectValue,
                Advanced = model.Advanced,
                ProjectDetails = model.ProjectDetails,
                /*Proposal = model.Proposal,
                WorkOrder = model.WorkOrder,
                SoftwareRequirement = model.SoftwareRequirement*/
            };
        }
    }
}
