using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Enums
{
    public enum DutyType
    {
        FullTime = 1,    // Hardcoded global business hours (e.g., standard corporate 9-5)
        Scheduled = 2,   // Fixed custom routine (e.g., always works Mon-Wed night shift)
        Roster = 3      // Dynamic shifts changing week by week
    }
}
