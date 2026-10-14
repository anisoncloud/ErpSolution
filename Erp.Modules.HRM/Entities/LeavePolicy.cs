using Erp.Core;
using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class LeavePolicy : BaseEntity
    {
        public LeaveType LeaveType { get; set; }
        /// <summary>Default days per calendar year for yearly types (Casual, Sick, Earned). 0 = no automatic yearly allowance.</summary>
        public int YearlyDays {  get; set; }
        /// <summary>&gt; 0: a job-life limit on how many times this leave may be taken (Pilgrimage 1, Maternity/Paternity 2).
        /// Such types have no yearly entitlement rows.</summary>
        public int LifetimeMaxTimes {  get; set; }
        /// <summary>&gt; 0: the application is always exactly this many days; the return date is calculated, not entered.</summary>
        public int FixedDays {  get; set; }
        /// <summary>Minimum days per application (Earned = 3). 0 = no minimum.</summary>
        public int MinDays {  get; set; }
        /// <summary>true: every calendar day counts (no holiday bridging rules).</summary>
        public bool CountAllCalendarDays {  get; set; }
        /// <summary>true: a cross-year range is split per calendar year; false: all days belong to the From date's year.</summary>
        public bool SplitByYear {  get; set; } = true;
        /// <summary>Uses the existing Employee <see cref="Gender"/> enum. null = available to everyone
        /// (Maternity = Female, Paternity = Male). Add a using for Gender's namespace if it is not in this one.</summary>
        public Gender? RequiredGender {  get; set; }
        /// <summary>Earned only: maximum Earned CF balance an employee can hold in their job life. 0 = no carry forward.</summary>
        public int CarryForwardCap {  get; set; }
    }
}
