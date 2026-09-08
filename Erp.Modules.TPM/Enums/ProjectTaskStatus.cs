using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Erp.Modules.TPM.Enums
{
    public enum ProjectTaskStatus
    {
        [EnumMember(Value = "Back Log for ")]
        Backlog = 1,          // Received from client, waiting in queue
        InDevelopment = 2,    // You are currently working on it
        ReadyForReview = 3,   // Handed over to the client for feedback
        ClientFeedback = 4,   // Client reviewed and requested changes/updates
        Completed = 5,
        [Display(Name = "Demo On Development ")]
        Demo = 6,
        [Display(Name = "Live On Server ")]
        LiveOnServer = 7,
        [Display(Name = "Live And Maintenance ")]
        LiveAndMaintenance = 8,
    }
}
