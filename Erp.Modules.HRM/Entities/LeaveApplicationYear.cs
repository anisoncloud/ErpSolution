using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class LeaveApplicationYear : BaseEntity
    {
        public int LeaveApplicationId {  get; set; }
        public LeaveApplication? LeaveApplication { get; set; }
        public int Year {  get; set; }
        public int Days { get; set; }
        public int WorkingDays {  get; set; }
        public int BridgeWeeklyDays {  get; set; }
        public int BridgePublicDays { get; set; }
        /// <summary>Of <see cref="Days"/>, how many were taken from the Earned CF balance (Earned leave only).</summary>
        public int CarryForwardDays {  get; set; }
    }
}
