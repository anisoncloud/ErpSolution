using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.Entities
{
    public class MotherCompany : BaseEntity
    {
        public string Name {  get; set; }
        public string? Description { get; set; }
        public ICollection<Company>? Companys { get; set; }
    }
}
