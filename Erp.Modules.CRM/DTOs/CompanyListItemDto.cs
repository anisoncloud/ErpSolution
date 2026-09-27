using Erp.Modules.CRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.DTOs
{
    public class CompanyListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NormalizedName { get; set; } = string.Empty;        
        public string? Description { get; set; }
        public string? CompanyEmail { get; set; }
        public string? CompanyPhone { get; set; }
        public string? CompanyAddress { get; set; }
    }
}
