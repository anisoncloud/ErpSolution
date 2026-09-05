using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.Entities
{
    public class TaskRevision : BaseEntity
    {
        // Foreign key to the parent task
        public int ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; } = null!;

        // What the client wants changed during this specific review cycle
        public string FeedbackNotes { get; set; } = string.Empty;

        // Track who added the feedback (Client or Developer note)
        public string FeedbackAddedBy { get; set; } = string.Empty;
        public DateTime FeedbackCreatedAt { get; set; } = DateTime.UtcNow;

        // Tracks if this specific round of updates has been completed
        public bool IsResolved { get; set; } = false;
        public DateTime? ResolvedAt { get; set; }
    }
}
