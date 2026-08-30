using Erp.Core;
using Erp.Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class LeaveRequest : BaseEntity
    {
        public int ID { get; set; }
        public string EmpId { get; set; }
        [ForeignKey("EmpId")]
        public ApplicationUser? Users { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Days { get; set; }
        public string LeaveStauts { get; set; } = "Pending";
        public string ManagerId { get; set; }
        public string ManagerEmail { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public DateTime? DecidedDate { get; set; }
        public string? Comment { get; set; }
    }
}
