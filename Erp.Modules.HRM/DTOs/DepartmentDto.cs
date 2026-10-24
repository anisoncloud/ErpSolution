using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? DepartmentCode { get; set; }
        public int? HeadEmployeeId { get; set; }
        public string? HeadName { get; set; }      // optional, to show the current head
        public bool IsActive { get; set; }
        }
}
