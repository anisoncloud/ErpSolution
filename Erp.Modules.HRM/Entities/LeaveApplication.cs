using Erp.Core;
using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class LeaveApplication : BaseEntity
    {
        public int EmployeeId {  get; set; }
        public Employee? Employee { get; set; }
        public LeaveType LeaveType { get; set; }
        /// <summary>First day of leave.</summary>
        public DateOnly FromDate {  get; set; }
        /// <summary>First day back at work. The last leave day is ReturnDate - 1. Calculated for fixed-day leaves.</summary>
        public DateOnly ReturnDate {  get; set; }
        /// <summary>Total days counted for this application.</summary>
        public int TotalDays {  get; set; }
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
        public int DecidedById { get; set;  }
        public Employee? DecidedBy {  get; set; }
        public string? ManagerComment { get; set; }
        public ICollection<LeaveApplicationYear> Years { get; set; } = new List<LeaveApplicationYear>();

    }
}
