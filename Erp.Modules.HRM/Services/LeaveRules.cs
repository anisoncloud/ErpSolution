using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public class LeaveRules
    {
    }
    public static class LeavePolicyRules
    {
        /// <summary>Fixed-day leave (Pilgrimage 30, Maternity 180, Paternity 7): the return date is the From date plus the
        /// fixed days, so a 30-day leave starting on the 1st has its 30th day on the 30th and the return date on the 31st.</summary>
        public static DateOnly ReturnDateFor(DateOnly from, int fixedDays) => from.AddDays(fixedDays);

        /// <summary>null requirement = everyone. Otherwise the employee's Gender must match exactly, so an employee
        /// whose Gender is Other is eligible for neither Maternity (Female) nor Paternity (Male).</summary>
        public static bool IsEligible(Gender? required, Gender? employeeGender) =>
            required is null || required == employeeGender;
    }

    public static class EarnedCarryForward
    {
        /// <summary>Earned CF balance carried into the next year.
        /// = (CF that was granted this year and not used) + (this year's Earned allowance that was not used),
        /// limited to <paramref name="cap"/> (the CF balance an employee can hold in their job life). cap 0 = no carry forward.</summary>
        /// <param name="earnedUsedFromYear">Earned days used from the year's own allowance (excluding days taken from CF).</param>
        public static int Next(int cfGranted, int cfUsed, int earnedTotal, int earnedUsedFromYear, int cap)
        {
            if (cap <= 0) return 0;
            var cfLeft = Math.Max(0, cfGranted - cfUsed);
            var unused = Math.Max(0, earnedTotal - earnedUsedFromYear);
            return Math.Min(cap, cfLeft + unused);
        }
    }
}
