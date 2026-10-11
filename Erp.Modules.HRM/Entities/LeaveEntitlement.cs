using Erp.Core;
using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class LeaveEntitlement : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int Year { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public LeaveType LeaveType { get; set; }
        public int TotalDays { get; set; }
    }
}
