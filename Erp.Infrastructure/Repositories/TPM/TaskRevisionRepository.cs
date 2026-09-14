using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.TPM.Entities;
using Erp.Modules.TPM.Repositories;
using Erp.Modules.TPM.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.TPM
{
    public class TaskRevisionRepository : GenericRepository<TaskRevision>, ITaskRevisionRepository
    {
        public TaskRevisionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
