using Erp.Modules.TPM.Entities;
using Erp.Modules.TPM.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Erp.Modules.TPM.ViewModels
{
    public class ProjectItemDetailsViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
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
        public ProjectItemType? ProjectItemType { get; set; }
        public ProjectItemStatus? ProjectItemStatus { get; set; }
        public ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
        public bool? LiveOnServer { get; set; }
        public DateOnly? LiveServerDate { get; set; }
        public bool? Maintenance { get; set; }
        public DateOnly? MaintStartDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? MaintValue { get; set; }
        public int? DomainsId { get; set; }
        public string DomainName { get; set; } 
        public string? IpAddress { get; set; }
        public string? Hosting { get; set; }
        public string? DomainRegistrant { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RegistarDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastUpdated { get; set; }
        public int? ForYear { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ExpireDate { get; set; }
        public string? Dns { get; set; }
        public string? Analytics { get; set; }
        public int? CompanyId { get; set; }
        
    }
}
