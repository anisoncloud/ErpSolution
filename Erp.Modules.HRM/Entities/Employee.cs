using Erp.Core;
using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class Employee : BaseEntity
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();

        // Loose coupling to Identity: store the ApplicationUser's Id as a plain Guid.
        // HRM module does NOT reference ERP.Core.Identity — no FK constraint, no navigation.
        // This keeps HRM's schema self-contained and swappable.
        public Guid? UserId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int DesignationId { get; set; }
        public Designation? Designation { get; set; }
        public int CompanyId {  get; set; }
        public Company? Company { get; set; }
        public EmployeeLevel Level { get; set; } = EmployeeLevel.Executive;
        public DateTime JoiningDate { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }
        
    }
}
