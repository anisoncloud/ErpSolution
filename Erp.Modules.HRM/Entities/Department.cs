using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class Department : BaseEntity
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
