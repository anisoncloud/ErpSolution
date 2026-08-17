using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class LeaveType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool IsEarned { get; set; }
        public int CarryForwardLimit { get; set; } = 5;
        public ICollection<LeaveEntitlement>? Entitlements { get; set; }
    }
}
