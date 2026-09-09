using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.DTOs
{
    public record TaskCreateDto(string Title, string Description, int ProjectId, DateTime? TargetDate);
}
