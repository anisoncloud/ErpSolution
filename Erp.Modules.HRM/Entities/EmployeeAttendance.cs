using Erp.Core;
using Erp.Core.Identity;
using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class EmployeeAttendance : BaseEntity
    {
        public int? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public string? CustomEmployeeId { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? InTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? OutTime { get; set; }
        public bool IsManualEntry { get; set; } = false;
        public string? Remarks { get; set; }
        public string? DeviceOrIpAddress { get; set; }
        // Status tracking (e.g., Present, Late)
        public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;
    }
}
