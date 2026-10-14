using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Enums
{
    public enum LeaveType
    {
        Casual = 1,
        Sick = 2,
        Earned = 3,
        Pilgrimage = 4,
        Maternity = 5,
        Paternity = 6,
        Compensation = 7,

        /// <summary>Ledger-only type: Earned days carried forward into a year. It is never applied for directly;
        /// Earned applications draw on it after the year's own Earned days are used up.</summary>
        /// 
        EarnedCarryForward = 8
    }
}
