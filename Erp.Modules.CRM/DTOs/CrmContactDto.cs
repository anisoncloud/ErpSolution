using Erp.Modules.CRM.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.CRM.DTOs
{
    public class CrmContactDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Designation { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Photo { get; set; }
        public string? Comments { get; set; }
        public int CrmCompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
