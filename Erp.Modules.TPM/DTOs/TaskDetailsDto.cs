using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.DTOs
{
    public record TaskDetailsDto(
        int Id,
        string Title,
        string Description,
        string Status,
        DateTime CreatedAt,
        DateTime? TargetDate,
        List<TaskRevisionDto> Revisions
        );
    
}
