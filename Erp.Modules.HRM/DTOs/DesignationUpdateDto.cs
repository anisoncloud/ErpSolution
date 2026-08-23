using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.DTOs
{
    public class DesignationUpdateDto
    {
        public int Id { get; set; } = new();
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? DesignationCode { get; set; }
        public bool IsActive { get; set; }
    }
}
