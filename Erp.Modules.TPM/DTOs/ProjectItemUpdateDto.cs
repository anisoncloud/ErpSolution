using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.TPM.DTOs
{
    public class ProjectItemUpdateDto
    {
        public DateOnly? DemoStartDate { get; set; }
        public DateOnly? WorkOrderDate { get; set; }
        public int? ProjectDays { get; set; }
        public DateOnly? ProjectDeliveryDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ProjectValue { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Advanced { get; set; }
        public string? ProjectDetails { get; set; }
    }
}
