using Erp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.Repositories
{
    public interface ITpmUnitOfWork : IUnitOfWork
    {
        IProjectItemRepository ProjectItems { get; }
        IProjectTaskRepository ProjectTasks { get; }
        ITaskRevisionRepository TaskRevisions { get; }
    }
}
