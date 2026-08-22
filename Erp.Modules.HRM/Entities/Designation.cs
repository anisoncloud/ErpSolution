using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class Designation : BaseEntity
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;      // e.g. "Sr. Software Engineer"
        public string? DesignationCode {  get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
