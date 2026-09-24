using Erp.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.CRM.Entities
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public string? Designation { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Photo { get; set; }
        public string? Comments { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("CrmCompanyId")]
        public Company? Company { get; set; }
    }
}
