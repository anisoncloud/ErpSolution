using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.DTOs
{
    public class DepartmentUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? DepartmentCode { get; set; }
        public bool IsActive { get; set; }
    }
}
