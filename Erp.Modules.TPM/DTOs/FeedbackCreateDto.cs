using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.DTOs
{
    public record FeedbackCreateDto(
        int ProjectTaskId, 
        string FeedbackNotes, 
        string CreatedBy
        );
    
}
