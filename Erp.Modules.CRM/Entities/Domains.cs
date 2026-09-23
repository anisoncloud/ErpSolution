using Erp.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.CRM.Entities
{
    public class Domains : BaseEntity
    {
        public string Name { get; set; }
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
        /*public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public CrmCompany? CrmCompany { get; set; }*/
        public string? Comments { get; set; }
    }
}
