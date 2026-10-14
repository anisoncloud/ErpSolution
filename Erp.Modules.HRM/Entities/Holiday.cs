using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class Holiday : BaseEntity
    {
        public DateOnly Date {  get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
