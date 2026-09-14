using Erp.Modules.TPM.DTOs;
using Erp.Modules.TPM.Entities;
using Erp.Modules.TPM.Enums;
using Erp.Modules.TPM.MappingDto;
using Erp.Modules.TPM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly ITpmUnitOfWork _uow;
        

        public ProjectTaskService(ITpmUnitOfWork uow)
        {
            _uow = uow;           
        }

        public async Task<bool> AddClientFeedbackAsync(FeedbackCreateDto dto)
        {
            var projectTask = await _uow.ProjectTasks.GetByIdAsync(dto.ProjectTaskId);
            if (projectTask == null)
            {
                return false;
            }
            projectTask.Status = ProjectTaskStatus.ClientFeedback;
            TaskRevision taskRevision = dto.ToModel();
            await _uow.TaskRevisions.AddAsync(taskRevision);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<int> CreateTaskAsync(TaskCreateDto dto)
        {
            ProjectTask task = dto.ToModel();
            await _uow.ProjectTasks.AddAsync(task);
            await _uow.SaveChangesAsync();
            return task.Id;            
        }

        public async Task<TaskDetailsDto?> GetTaskDetailsAsync(int taskId)
        {
            var task = await _uow.ProjectTasks.GetProjectTaskWithRevisionAsync(taskId);
            if (task==null)
            {
                return null;
            }
            return task.ToDetailsDto();
        }

        public async Task<bool> ResolveRevisionAsync(int revisionId)
        {
            var revision = await _uow.TaskRevisions.GetByIdAsync(revisionId);
            if (revision == null)
            {
                return false;
            }
            revision.IsResolved = true;
            revision.ResolvedAt = DateTime.UtcNow;
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int taskId, ProjectTaskStatus newStatus)
        {
            var projectTask = await _uow.ProjectTasks.GetByIdAsync(taskId);
            if (projectTask == null)
            {
                return false;
            }
            projectTask.Status = newStatus;
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
