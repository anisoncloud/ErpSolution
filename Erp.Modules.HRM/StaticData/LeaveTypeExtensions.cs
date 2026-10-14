using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.StaticData
{
    public static class LeaveTypeExtensions
    {
        public static bool IsApplicable(this LeaveType t)
        {
           return t != LeaveType.EarnedCarryForward;
        }
        public static string Display(this LeaveType t)
        {
            return t switch
            {
                LeaveType.EarnedCarryForward => "Earned CF",
                LeaveType.Compensation => "Compasation",
                _ => t.ToString()
            };
        }
            
    }
}
