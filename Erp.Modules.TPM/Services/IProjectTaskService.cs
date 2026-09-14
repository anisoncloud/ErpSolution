using Erp.Modules.TPM.DTOs;
using Erp.Modules.TPM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.Services
{
    public interface IProjectTaskService
    {
        Task<int> CreateTaskAsync(TaskCreateDto dto);
        Task<TaskDetailsDto?> GetTaskDetailsAsync(int taskId);
        Task<bool> AddClientFeedbackAsync(FeedbackCreateDto dto);
        Task<bool> UpdateStatusAsync(int taskId, ProjectTaskStatus newStatus);
        Task<bool> ResolveRevisionAsync(int revisionId);
    }
}
