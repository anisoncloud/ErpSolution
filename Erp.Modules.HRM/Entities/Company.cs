using Erp.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? CompanyCode { get; set; }
        public string? Description { get; set; }
        public string? CompanyLogo {  get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
