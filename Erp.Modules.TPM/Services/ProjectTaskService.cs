using Erp.Modules.TPM.DTOs;
using Erp.Modules.TPM.Entities;
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

        public Task<bool> AddClientFeedbackAsync(FeedbackCreateDto dto)
        {
            throw new NotImplementedException();
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
            var task = await _uow.ProjectTasks
                
            throw new NotImplementedException();
        }

        public Task<bool> ResolveRevisionAsync(int revisionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStatusAsync(int taskId, TaskStatus newStatus)
        {
            throw new NotImplementedException();
        }
    }
}
