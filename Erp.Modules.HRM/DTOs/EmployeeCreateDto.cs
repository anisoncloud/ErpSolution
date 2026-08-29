using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Erp.Modules.HRM.DTOs
{
    public class EmployeeCreateDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special characters are not allowed.")]
        public string EmployeeCode { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special characters are not allowed.")]
        public string FullName { get; set; } = string.Empty;
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        [Required]
        public int CompanyId {  get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int DesignationId { get; set; }
        public EmployeeLevel Level { get; set; }
        [Required]
        public DateTime JoiningDate { get; set; }
        [Required]
        public decimal? Salary { get; set; }
        public Guid UserId { get; set; }   // set after Identity user is created
        public bool IsActive {  get; set; }
        public DateTime CreatedAt {  get; set; }
    }
}
