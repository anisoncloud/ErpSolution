using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.DTOs
{
    public record TaskRevisionDto(
        int Id, 
        string FeedbackNotes, 
        string CreatedBy, 
        DateTime CreatedAt, 
        bool IsResolved
        );
    
}
