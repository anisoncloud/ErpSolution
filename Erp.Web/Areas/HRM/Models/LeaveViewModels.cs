using Erp.Modules.HRM.Enums;
using Erp.Modules.HRM.Repositories;
using Erp.Modules.HRM.Services;
using System.ComponentModel.DataAnnotations;

namespace Erp.Web.Areas.HRM.Models
{
    public class LeaveViewModels
    {
    }
    public class LeaveApplyViewModel
    {
        [Required, Display(Name = "Leave type")]
        public LeaveType? LeaveType { get; set; }

        [Required, Display(Name = "From date")]
        public DateOnly? FromDate { get; set; }

        /// <summary>Ignored for fixed-day leave (Pilgrimage, Maternity, Paternity): the server calculates it.</summary>
        [Display(Name = "Return date")]
        public DateOnly? ReturnDate { get; set; }
    }

    public class LeaveConfirmViewModel
    {
        public LeaveApplyViewModel Input { get; set; } = new();
        public LeavePreview Preview { get; set; } = null!;
    }

    public class LeaveApprovalsViewModel
    {
        public IReadOnlyList<PendingLeaveItem> Items { get; set; } = Array.Empty<PendingLeaveItem>();
    }

    public class HolidayFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public DateOnly? Date { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; } = string.Empty;
    }

    public class HolidayIndexViewModel
    {
        public int Year { get; set; }
        public IReadOnlyList<HolidayDto> Holidays { get; set; } = Array.Empty<HolidayDto>();
        public HolidayFormViewModel NewHoliday { get; set; } = new();
    }

    public class LeavePolicyViewModel
    {
        public LeaveType LeaveType { get; set; }

        [Range(0, 365), Display(Name = "Yearly days")]
        public int YearlyDays { get; set; }

        [Range(0, 20), Display(Name = "Job-life limit (times, 0 = unlimited)")]
        public int LifetimeMaxTimes { get; set; }

        [Range(0, 1000), Display(Name = "Fixed days (0 = employee picks the dates)")]
        public int FixedDays { get; set; }

        [Range(0, 365), Display(Name = "Minimum days per application")]
        public int MinDays { get; set; }

        [Display(Name = "Count every calendar day (no holiday bridging)")]
        public bool CountAllCalendarDays { get; set; }

        [Display(Name = "Split a cross-year range per year")]
        public bool SplitByYear { get; set; }

        [Display(Name = "Available to (empty = anyone)")]
        public Gender? RequiredGender { get; set; }

        [Range(0, 1000), Display(Name = "Earned CF limit in job life (Earned only)")]
        public int CarryForwardCap { get; set; }

        public static LeavePolicyViewModel From(LeavePolicyDto d) => new()
        {
            LeaveType = d.LeaveType,
            YearlyDays = d.YearlyDays,
            LifetimeMaxTimes = d.LifetimeMaxTimes,
            FixedDays = d.FixedDays,
            MinDays = d.MinDays,
            CountAllCalendarDays = d.CountAllCalendarDays,
            SplitByYear = d.SplitByYear,
            RequiredGender = d.RequiredGender,
            CarryForwardCap = d.CarryForwardCap
        };

        public LeavePolicyDto ToDto() => new(LeaveType, YearlyDays, LifetimeMaxTimes, FixedDays, MinDays,
            CountAllCalendarDays, SplitByYear, RequiredGender, CarryForwardCap);
    }

    public class EntitlementIndexViewModel
    {
        public int Year { get; set; }
        public IReadOnlyList<EmployeeRef> Employees { get; set; } = Array.Empty<EmployeeRef>();
    }

    public class EntitlementEditViewModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public int Year { get; set; }

        [Range(0, 365)] public int Casual { get; set; }
        [Range(0, 365)] public int Sick { get; set; }
        [Range(0, 365)] public int Earned { get; set; }

        [Range(0, 1000), Display(Name = "Earned CF")]
        public int EarnedCarryForward { get; set; }

        [Range(0, 365)] public int Compensation { get; set; }
    }

    public class CompensationViewModel
    {
        [Required, Display(Name = "Employee")]
        public int? EmployeeId { get; set; }

        [Required, Display(Name = "Year")]
        public int Year { get; set; }

        [Range(1, 365), Display(Name = "Days to add")]
        public int Days { get; set; } = 1;

        [Required, StringLength(500)]
        public string Reason { get; set; } = string.Empty;

        public IReadOnlyList<EmployeeRef> Team { get; set; } = Array.Empty<EmployeeRef>();
    }
}
