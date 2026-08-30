using Erp.Core;
using Erp.Core.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class EmployeeAttendance : BaseEntity
    {
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public ApplicationUser Users { get; set; }
        public string CustomEmployeeId { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? InTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? OutTime { get; set; }
    }
}
