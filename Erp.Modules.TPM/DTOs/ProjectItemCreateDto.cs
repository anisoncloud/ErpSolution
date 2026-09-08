using Erp.Modules.TPM.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.TPM.DTOs
{
    public class ProjectItemCreateDto
    {
        [Display(Name="Project Name", Prompt = "Enter Project Name here")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Demo Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateOnly? DemoStartDate { get; set; }
        [Display(Name="Work Order Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateOnly? WorkOrderDate { get; set; }
        [DisplayName("Project Days")]
        public int? ProjectDays { get; set; }
        [DisplayName("Project Delivery Date")]
        public DateOnly? ProjectDeliveryDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [DisplayName("Project Cost")]
        public decimal? ProjectValue { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [DisplayName("Advanced Payment")]
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
        //public string? Proposal { get; set; }
        //public string? WorkOrder { get; set; }
        //public string? SoftwareRequirement { get; set; }
        public IFormFile? WorkOrder { get; set; }
        public IFormFile? Proposal { get; set; }
        public IFormFile? SoftwareRequirement { get; set; }
        public ProjectTaskStatus Status { get; set; }
    }
}
