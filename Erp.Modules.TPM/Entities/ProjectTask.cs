using Erp.Core;
using Erp.Modules.TPM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.Entities
{
    public class ProjectTask : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Links the task to a specific project
        public int ProjectId { get; set; }
        public ProjectItem ProjectItem { get; set; } = null!;

        // Current workflow status
        public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.Backlog;

        public DateTime TaskGivenDate { get; set; } = DateTime.UtcNow;
        public DateTime? TaskDeliveryDate { get; set; }

        // Navigation property to track all client revisions/updates for this specific task
        public ICollection<TaskRevision> Revisions { get; set; } = new List<TaskRevision>();
    }
}
