using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Erp.Modules.TPM.DTOs
{
    public record ProjectItemMaintenanceEditDto(
        int Id,
        [Required]
        string Name,
        [Required]
        string? Description,
        [Required(ErrorMessage = "Live server transition timestamp is required.")]
        DateOnly? LiveServerDate,
        [Required(ErrorMessage = "Maintenance initialization date is required.")]
        DateOnly? MaintenanceStartDate,
        [Required(ErrorMessage = "Maintenance cost valuation is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        decimal? MaintenanceAmount
        );
    
}
