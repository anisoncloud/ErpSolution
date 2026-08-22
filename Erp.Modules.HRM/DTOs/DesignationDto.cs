using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.DTOs
{
    public class DesignationDto
    {
        public Guid PublicId { get; set; }
        public int Id { get; set; } = new();
        public string? DesignationCode {  get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsActive {  get; set; }
        public string? Description { get; set; }
    }
}
