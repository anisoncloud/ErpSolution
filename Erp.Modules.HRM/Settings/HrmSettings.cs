using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Erp.Modules.HRM.Settings
{
    public class HrmSettings
    {
        public const string Section = "Hrm";
        /// <summary>Weekly holidays. Default Friday + Saturday.</summary>
        public DayOfWeek[] WeeklyHolidays { get; set;  } = {DayOfWeek.Friday, DayOfWeek.Saturday};

        /// <summary>An application must be submitted within this many days of its date.</summary>
        public int ApplicaWindowsDays { get; set; } = 15;

        /// <summary>Time zone used for "today" (IANA or Windows id).</summary>
        /// 
        public string TimeZoneId { get; set; } = "Asia/Dhaka";


        /// <summary>Default yearly entitlement per leave type (editable per employee by the Admin).</summary>

        public Dictionary<LeaveType, int> DefaultEntitlements { get; set; } = new()
        {
            [LeaveType.Casual] = 10,
            [LeaveType.Sick] = 14,
            [LeaveType.Earned] = 18,
            [LeaveType.Pilgrimage] = 30
        };
        public IReadOnlySet<DayOfWeek> WeeklyHolidaySet => WeeklyHolidays.ToHashSet();
    }
}
