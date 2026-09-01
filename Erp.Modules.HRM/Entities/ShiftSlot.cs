using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class ShiftSlot : BaseEntity
    {
        public string SlotName { get; set; } // e.g., "Morning Shift", "Night Shift", "Part-Time Evening"

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
