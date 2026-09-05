using Erp.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.TPM.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }  = string.Empty;
        public DateOnly? DemoStartDate { get; set; }
        public DateOnly? WorkOrderDate { get; set; }
        public int? ProjectDays { get; set; }
        public DateOnly? ProjectDeliveryDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ProjectValue { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Advanced { get; set; }
        public string? ProjectDetails { get; set; }
        public string? Comments { get; set; }
        public decimal? DuePayment
        {
            get
            {
                return ProjectValue - Advanced;
            }
        }
        public string? Proposal { get; set; }
        public string? WorkOrder { get; set; }
        public string? SoftwareRequirement { get; set; }
    }
}
