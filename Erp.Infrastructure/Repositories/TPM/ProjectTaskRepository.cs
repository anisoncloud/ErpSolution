using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.TPM.Entities;
using Erp.Modules.TPM.Repositories;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.TPM
{
    public class ProjectTaskRepository : GenericRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ProjectTask?> GetProjectTaskWithRevisionAsync(int taskId)
        {
            return await _context.ProjectTasks
                .Include(t => t.Revisions)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }
        
    }
}
