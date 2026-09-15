using Erp.Core.Interfaces;
using Erp.Modules.TPM.DTOs;
using Erp.Modules.TPM.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Erp.Modules.TPM.Repositories
{
    public interface IProjectItemRepository : IGenericRepository<ProjectItem>
    {
        Task<List<ProjectItem>> GetAllProjectItemWithTasks();
        Task<ProjectItem?> GetProjectTaskLogsAsync(int projectId);
    }
}
