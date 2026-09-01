using Erp.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class SalaryPositionHistory : BaseEntity
    {
        public string UserId { get; set; } // FK to EmployeeProfile
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public decimal BaseSalary { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string Notes { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
