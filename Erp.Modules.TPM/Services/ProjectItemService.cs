using Erp.Core.Interfaces;
using Erp.Modules.TPM.DTOs;
using Erp.Modules.TPM.Entities;
using Erp.Modules.TPM.Enums;
using Erp.Modules.TPM.MappingDto;
using Erp.Modules.TPM.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Erp.Modules.TPM.Services
{
    public class ProjectItemService : IProjectItemService
    {
        private readonly ITpmUnitOfWork _uow;
        private readonly IFileStorageService _fileStorageService;
        public ProjectItemService(ITpmUnitOfWork uow, IFileStorageService fileStorageService)
        {
            _uow = uow;
            _fileStorageService = fileStorageService; 
        }
        public async Task<ProjectItemDto> CreateProjectItem(ProjectItemCreateDto dto)
        {
            string? proposalFileUrl = null;
            string? workOrderFileUrl = null;
            string? requirementFileUrl = null;
            if (dto.Proposal != null && dto.Proposal.Length > 0)
            {
                // Upload via the shared service layer
                    proposalFileUrl = await _fileStorageService.UploadFileAsync(
                    file: dto.Proposal,
                    moduleName: "tpm",
                    subFolder: "ProjectItems",
                    //customFileName: $"item_{dto.ItemCode}", // Rename to match item code
                    customFileName: $"item_proposal_{dto.Name}", // Rename to match item code
                    allowedExtensions: new[] { ".pdf", ".jpg", ".png" }
                );
            }
            if (dto.WorkOrder != null && dto.WorkOrder.Length > 0)
            {
                // Upload via the shared service layer
                    workOrderFileUrl = await _fileStorageService.UploadFileAsync(
                    file: dto.WorkOrder,
                    moduleName: "tpm",
                    subFolder: "ProjectItems",
                    //customFileName: $"item_{dto.ItemCode}", // Rename to match item code
                    customFileName: $"item_workorder_{dto.Name}", // Rename to match item code
                    allowedExtensions: new[] { ".pdf", ".jpg", ".png" }
                );
            }
            if (dto.SoftwareRequirement != null && dto.SoftwareRequirement.Length > 0)
            {
                // Upload via the shared service layer
                requirementFileUrl = await _fileStorageService.UploadFileAsync(
                file: dto.SoftwareRequirement,
                moduleName: "tpm",
                subFolder: "ProjectItems",
                //customFileName: $"item_{dto.ItemCode}", // Rename to match item code
                customFileName: $"item_requirement_{dto.Name}", // Rename to match item code
                allowedExtensions: new[] { ".pdf", ".jpg", ".png" }
            );
            }
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
                Proposal = proposalFileUrl,
                WorkOrder = workOrderFileUrl,
                SoftwareRequirement = requirementFileUrl
            };
            await _uow.ProjectItems.AddAsync(model);
            await _uow.SaveChangesAsync();
            return model.ToDto();
        }
        public async Task<IEnumerable<ProjectItemDto>> GetProjectsAsync()
        {
            var model = await _uow.ProjectItems.GetAllAsync(x=>x.OrderByDescending(x=>x.CreatedAt));
            return model.ToListDto();
        }

        public async Task<IEnumerable<ProjectItemDto>> GetAllProjectWithOutMaintenance()
        {
            var model = await _uow.ProjectItems.GetAllWithOutFilter()
                .Where(x=>x.ProjectItemStatus != ProjectItemStatus.Maintenance)
                .OrderByDescending(x=>x.CreatedAt)                
                .ToListAsync();
            return model.ToListDto();
        }
        public async Task<List<ProjectDashboardDto>> GetAllProjectsWithTasksAsync()
        {
            var projectItems = await _uow.ProjectItems.GetAllProjectItemWithTasks();
            return projectItems.Select(p => p.ToDashboardDto()).ToList();
        }
        public async Task<ProjectDashboardDto?> GetProjectTaskLogsAsync(int projectId)
        {
            var projectItem = await _uow.ProjectItems.GetProjectTaskLogsAsync(projectId);
            if (projectItem ==null)
            {
                return null;
            }
            return projectItem.ToDashboardDto();
        }

        public async Task<ProjectItemMaintenanceEditDto?> GetProjectForMaintenanceEditAsync(int projectId)
        {
            var projectItem = await _uow.ProjectItems.GetByIdAsync(projectId);
            if (projectItem==null)
            {
                return null;
            }
            return projectItem.ToMaintenanceEditDto();
        }

        public async Task<bool> UpdateProjectToMaintenanceAsync(ProjectItemMaintenanceEditDto dto)
        {
            var projectItem = await _uow.ProjectItems.GetByIdAsync(dto.Id);
            if (projectItem == null) return false;
            // Apply fields and transition status
            projectItem.ProjectItemStatus = ProjectItemStatus.Maintenance;
            projectItem.LiveServerDate = dto.LiveServerDate;
            projectItem.MaintStartDate = dto.MaintenanceStartDate;
            projectItem.MaintValue = dto.MaintenanceAmount;

            await _uow.ProjectItems.UpdateAsync(projectItem);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
