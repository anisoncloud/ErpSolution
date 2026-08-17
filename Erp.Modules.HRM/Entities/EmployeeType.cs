using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class EmployeeType : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description {  get; set; }
    }
}
