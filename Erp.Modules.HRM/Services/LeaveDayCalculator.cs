using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public sealed record LeaveYearDays(int Year, int Working, int BridgedWeekly, int BridgedPublic)
    {
        public int Total => Working + BridgedWeekly + BridgedPublic;
    }

    public sealed record LeaveDayResult(
        LeaveType Type,
        DateOnly From,
        DateOnly LastLeaveDay,
        IReadOnlyList<LeaveYearDays> ByYear)
    {
        public int TotalDays => ByYear.Sum(y => y.Total);
        public int WorkingDays => ByYear.Sum(y => y.Working);
        public int BridgedWeeklyDays => ByYear.Sum(y => y.BridgedWeekly);
        public int BridgedPublicDays => ByYear.Sum(y => y.BridgedPublic);
    }

    /// <summary>How days are counted for one leave type (comes from LeavePolicy).</summary>
    public sealed record LeaveCountingRules(bool CountAllCalendarDays, bool SplitByYear)
    {
        /// <summary>Fallback when no policy is supplied (used by tests): Pilgrimage, Maternity and Paternity count every
        /// calendar day in one block; everything else bridges holidays and splits per year.</summary>
        public static LeaveCountingRules Default(LeaveType type) =>
            type is LeaveType.Pilgrimage or LeaveType.Maternity or LeaveType.Paternity
                ? new LeaveCountingRules(true, false)
                : new LeaveCountingRules(false, true);
    }

    public interface ILeaveDayCalculator
    {
        /// <param name="returnDate">First day back at work; the last leave day is returnDate - 1.</param>
        LeaveDayResult Calculate(
            LeaveType type,
            DateOnly from,
            DateOnly returnDate,
            IReadOnlySet<DayOfWeek> weeklyHolidays,
            IReadOnlySet<DateOnly> publicHolidays,
            LeaveCountingRules? rules = null);
    }
    public sealed class LeaveDayCalculator : ILeaveDayCalculator
    {
        public LeaveDayResult Calculate(
        LeaveType type,
        DateOnly from,
        DateOnly returnDate,
        IReadOnlySet<DayOfWeek> weeklyHolidays,
        IReadOnlySet<DateOnly> publicHolidays,
        LeaveCountingRules? rules = null)
        {
            if (returnDate <= from)
                throw new ArgumentException("Return date must be after the From date.", nameof(returnDate));

            rules ??= LeaveCountingRules.Default(type);
            var last = returnDate.AddDays(-1);
            bool IsHoliday(DateOnly d) => publicHolidays.Contains(d) || weeklyHolidays.Contains(d.DayOfWeek);

            DateOnly? start = from, end = last;
            if (!rules.CountAllCalendarDays)
            {
                start = null;
                end = null;
                for (var d = from; d <= last; d = d.AddDays(1))
                    if (!IsHoliday(d)) { start = d; break; }

                if (start is not null)
                    for (var d = last; d >= start.Value; d = d.AddDays(-1))
                        if (!IsHoliday(d)) { end = d; break; }
            }

            var perYear = new SortedDictionary<int, int[]>(); // [working, bridgedWeekly, bridgedPublic]
            if (start is not null && end is not null)
            {
                for (var d = start.Value; d <= end.Value; d = d.AddDays(1))
                {
                    var key = rules.SplitByYear ? d.Year : from.Year;
                    if (!perYear.TryGetValue(key, out var c)) perYear[key] = c = new int[3];
                    if (publicHolidays.Contains(d)) c[2]++;
                    else if (weeklyHolidays.Contains(d.DayOfWeek)) c[1]++;
                    else c[0]++;
                }
            }

            var years = perYear.Select(kv => new LeaveYearDays(kv.Key, kv.Value[0], kv.Value[1], kv.Value[2])).ToList();
            return new LeaveDayResult(type, from, last, years);
        }
    }
}
