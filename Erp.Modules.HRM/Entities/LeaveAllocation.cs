using Erp.Core;
using Erp.Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class LeaveAllocation : BaseEntity
    {
        public int ID { get; set; }
        public string? EmpId { get; set; }
        [ForeignKey("EmpId")]
        public ApplicationUser? Users { get; set; }
        public int? Sick { get; set; }
        public int? Casual { get; set; }
        public int? Earned { get; set; }
        public int? CarryForward { get; set; }
        public int? Maternity { get; set; }
        public int? Paternity { get; set; }
        public int? Pilgrimage { get; set; }
        public int? Compensation { get; set; }
    }
}
