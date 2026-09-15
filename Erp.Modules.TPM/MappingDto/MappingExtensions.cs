using Erp.Modules.TPM.DTOs;
using Erp.Modules.TPM.Entities;
using Erp.Modules.TPM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.MappingDto
{
    public static class MappingExtensions
    {
        public static TaskRevisionDto ToDto(this TaskRevision revision)
        {
            return new TaskRevisionDto(
                Id: revision.Id,
                FeedbackNotes: revision.FeedbackNotes,
                CreatedBy: revision.CreatedBy,
                CreatedAt: revision.CreatedAt,
                IsResolved: revision.IsResolved
            );
        }
        public static TaskRevision ToModel(this FeedbackCreateDto dto)
        {
            return new TaskRevision
            {
                ProjectTaskId = dto.ProjectTaskId,
                FeedbackNotes = dto.FeedbackNotes,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow, // Explicitly set defaults during conversion
                IsResolved = false
            };
        }

        public static ProjectTask ToModel(this TaskCreateDto dto)
        {
            return new ProjectTask
            {
                Title = dto.Title,
                Description = dto.Description,
                ProjectId = dto.ProjectId,
                TaskDeliveryDate = dto.TargetDate,
                Status = ProjectTaskStatus.Backlog, // Set default starting status
                CreatedAt = DateTime.UtcNow
            };
        }

        public static TaskDetailsDto ToDetailsDto(this ProjectTask task)
        {
            return new TaskDetailsDto(
                Id: task.Id,
                Title: task.Title,
                Description: task.Description,
                Status: task.Status.ToString(), // Convert Enum to String for safety
                CreatedAt: task.CreatedAt,
                TargetDate: task.TaskDeliveryDate,

                // Manually map each nested TaskRevision item in the collection
                Revisions: task.Revisions
                    .Select(r => r.ToDto())
                    .ToList()
            );
        }
        public static ProjectDashboardDto ToDashboardDto(this ProjectItem project)
        {
            return new ProjectDashboardDto(
                Id: project.Id,
                Name: project.Name,

                // Loop through each task assigned to this project and manually map it
                TasksDetails: project.ProjectTasks != null
                    ? project.ProjectTasks.Select(t => t.ToDetailsDto()).ToList()
                    : new List<TaskDetailsDto>()
            );
        }
    }
}
