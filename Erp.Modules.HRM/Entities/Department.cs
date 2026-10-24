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
        public string NormalizedName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? DepartmentCode {  get; set; }
        public int? HeadEmployeeId { get; set; }   // nullable: a new department has no head yet
        public Employee? Head { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
