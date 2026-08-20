using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Erp.Modules.HRM.DTOs
{
    public class CompanyEditDto
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special characters are not allowed.")]
        public string Name { get; set; } = default!;
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special characters are not allowed.")]
        public string? CompanyCode { get; set; }
        public string? Description { get; set; }
        public string? CompanyLogo { get; set; }
    }
}
