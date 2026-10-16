using Erp.Core;
using Erp.Core.Identity;
using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class LeaveRequest : BaseEntity
    {
        public int ID { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public LeaveType LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Days { get; set; }
        public string LeaveStauts { get; set; } = "Pending";
        public int ManagerId { get; set; }
        public Employee? Manager { get; set; }
        public string ManagerEmail { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public DateTime? DecidedDate { get; set; }
        public string? Comment { get; set; }
    }
}
