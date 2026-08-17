using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class LeaveEntitlement : BaseEntity
    {
        public int EmployeeTypeId { get; set; }
        public EmployeeType? EmployeeType { get; set; }
        public int LeaveTypeId { get; set; }
        public LeaveType? LeaveType { get; set; }
        public int DaysPerYer { get; set; }
    }
}
