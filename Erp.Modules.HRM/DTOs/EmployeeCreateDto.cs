using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.DTOs
{
    public class EmployeeCreateDto
    {

        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public EmployeeLevel Level { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal? Salary { get; set; }
        public Guid UserId { get; set; }   // set after Identity user is created
    }
}
