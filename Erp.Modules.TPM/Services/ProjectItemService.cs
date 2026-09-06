using Erp.Modules.TPM.DTOs;
using Erp.Modules.TPM.Entities;
using Erp.Modules.TPM.MappingDto;
using Erp.Modules.TPM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Erp.Modules.TPM.Services
{
    public class ProjectItemService : IProjectItemService
    {
        private readonly ITpmUnitOfWork _uow;
        public ProjectItemService(ITpmUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ProjectItemDto> CreateProjectItem(ProjectItemCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Project name can not be empty", nameof(dto.Name));
            }            
            
            var model = new ProjectItem
            {
                Name = dto.Name.Trim(),
                DemoStartDate = dto.DemoStartDate,
                WorkOrderDate = dto.WorkOrderDate,
                ProjectDays = dto.ProjectDays,
                ProjectDeliveryDate = dto.ProjectDeliveryDate,
                ProjectValue = dto.ProjectValue,
                Advanced = dto.Advanced,
                ProjectDetails = dto.ProjectDetails,
                Proposal = dto.Proposal,
                WorkOrder = dto.WorkOrder,
                SoftwareRequirement = dto.SoftwareRequirement
            };
            await _uow.ProjectItems.AddAsync(model);
            await _uow.SaveChangesAsync();
            return model.ToDto();
        }
    }
}
