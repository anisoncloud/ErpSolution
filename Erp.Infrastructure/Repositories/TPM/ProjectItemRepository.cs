using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Repositories;
using Erp.Modules.TPM.Entities;
using Erp.Modules.TPM.Repositories;
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
    }
}
