using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class EmployeeSalaryHistory : BaseEntity
    {
        public string UserId { get; set; } // Foreign key to EmployeeProfile
        public decimal SalaryAmount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
