using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class ScheduledDuty : BaseEntity
    {
        public string UserId { get; set; } // FK to EmployeeProfile
        public DayOfWeek DayOfWeek { get; set; } // Enforced system Enum (Sunday = 0, Monday = 1...)
        public int ShiftSlotId { get; set; } // FK to the Master Time Slot
        public virtual Employee Employee { get; set; }
        public virtual ShiftSlot ShiftSlot { get; set; }
    }
}
