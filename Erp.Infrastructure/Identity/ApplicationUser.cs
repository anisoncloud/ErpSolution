using Erp.Modules.HRM.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string EmployeeCode { get; set; } = string.Empty;        
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Employee Employee { get; set; }
    }
}
