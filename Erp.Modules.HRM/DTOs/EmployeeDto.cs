using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        public int DesignationId { get; set; }
        public string DesignationTitle { get; set; } = string.Empty;

        public EmployeeLevel Level { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal? Salary { get; set; }
        public bool IsActive { get; set; }
    }
}
