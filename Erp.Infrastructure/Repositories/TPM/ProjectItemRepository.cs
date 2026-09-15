using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Repositories;
using Erp.Modules.TPM.DTOs;
using Erp.Modules.TPM.Entities;
using Erp.Modules.TPM.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.TPM
{
    public class ProjectItemRepository : GenericRepository<ProjectItem>, IProjectItemRepository
    {
        public ProjectItemRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<ProjectItem>> GetAllProjectItemWithTasks()
        {
            return await _context.ProjectItems
                .Include(x => x.ProjectTasks)
                .ToListAsync();
        }

        public async Task<ProjectItem?> GetProjectTaskLogsAsync(int projectId)
        {
            return await _context.ProjectItems
                .Include(x => x.ProjectTasks)
                .ThenInclude(x=>x.Revisions)
                .FirstOrDefaultAsync(x=>x.Id == projectId);
        }
    }
}
