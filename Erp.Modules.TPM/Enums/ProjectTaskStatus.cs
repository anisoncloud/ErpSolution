using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.Enums
{
    public enum ProjectTaskStatus
    {
        Backlog = 1,          // Received from client, waiting in queue
        InDevelopment = 2,    // You are currently working on it
        ReadyForReview = 3,   // Handed over to the client for feedback
        ClientFeedback = 4,   // Client reviewed and requested changes/updates
        Completed = 5
    }
}
