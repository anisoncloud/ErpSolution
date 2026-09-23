using Erp.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.CRM.Entities
{
    public class Hosting : BaseEntity
    {
        public int DomainId { get; set; }
        public Domains? Domains { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
        public DateTime HostingStartDate { get; set; }
        public DateTime HostingExpireDate { get; set; }
        public DateTime HostingUpdatedDate { get; set; }
        public int HostingDuration { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PricePerYear { get; set; }
        public int Package { get; set; }
        public string? Comment { get; set; }
    }
}
