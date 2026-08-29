using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Erp.Modules.HRM.ViewModels
{
    public class EmployeeFormViewModel
    {
        public Guid UserId { get; set; }
        [Required]
        public string EmployeeCode { get; set; } = string.Empty;
        [Required] public string FullName { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        [Required] public int CompanyId { get; set; }
        [Required] public int DepartmentId { get; set; }
        [Required] public int DesignationId { get; set; }
        public string? DepartmentName { get; set; }
        [Required] public EmployeeLevel Level { get; set; } = EmployeeLevel.Executive;
        [DataType(DataType.Date)] public DateTime JoiningDate { get; set; } = DateTime.UtcNow;
        public decimal? Salary { get; set; }
        [DataType(DataType.Password)] public string TemporaryPassword { get; set; } = "Password@123";
    }
}
