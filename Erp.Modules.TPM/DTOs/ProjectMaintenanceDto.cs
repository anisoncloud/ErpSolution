using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.DTOs
{
    public record ProjectMaintenanceDto(
        string Name,
        DateOnly? MaintStartDate,
        DateOnly? LiveServerDate,
        Decimal? MaintVale
        );
    
}
