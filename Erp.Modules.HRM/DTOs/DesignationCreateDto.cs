using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.DTOs
{
    public class DesignationCreateDto
    {
        public int Id { get; set; } = new();
        public string Title { get; set; } = string.Empty;
    }
}
