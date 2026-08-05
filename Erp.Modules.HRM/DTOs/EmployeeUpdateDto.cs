using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.DTOs
{
    public class EmployeeUpdateDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid DesignationId { get; set; }
        public EmployeeLevel Level { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal? Salary { get; set; }
    }
}
