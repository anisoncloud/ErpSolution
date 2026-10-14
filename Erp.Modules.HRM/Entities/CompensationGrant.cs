using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class CompensationGrant : BaseEntity
    {
        public int EmployeeId {  get; set; }
        public Employee? Employee { get; set; }
        public int Year {  get; set; }
        public int Days {  get; set; }
        public string Reason { get; set; } = string.Empty;
        public int GrantedById {  get; set; }
        public Employee? GrantedBy {  get; set; }
    }
}
