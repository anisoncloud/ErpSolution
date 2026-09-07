using Erp.Core.Interfaces;
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
        private readonly IFileStorageService _fileStorageService;
        public ProjectItemService(ITpmUnitOfWork uow, IFileStorageService fileStorageService)
        {
            _uow = uow;
            _fileStorageService = fileStorageService; 
        }
        public async Task<ProjectItemDto> CreateProjectItem(ProjectItemCreateDto dto)
        {
            string? fileUrl = null;
            if (dto.Proposal != null && dto.Proposal.Length > 0)
            {
                // Upload via the shared service layer
                fileUrl = await _fileStorageService.UploadFileAsync(
                    file: dto.Proposal,
                    moduleName: "tpm",
                    subFolder: "ProjectItems",
                    //customFileName: $"item_{dto.ItemCode}", // Rename to match item code
                    customFileName: $"item_{dto.Proposal}", // Rename to match item code
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
                Proposal = fileUrl,
                WorkOrder = dto.WorkOrder,
                SoftwareRequirement = dto.SoftwareRequirement
            };
            await _uow.ProjectItems.AddAsync(model);
            await _uow.SaveChangesAsync();
            return model.ToDto();
        }
        public async Task<IEnumerable<ProjectItemDto>> GetCompanyAsync()
        {
            var model = await _uow.ProjectItems.GetAllAsync();
            return model.ToListDto();
        }
    }
}
