using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        //public int MotherCompanyId {  get; set; }
        //public MotherCompany? MotherCompany { get; set; }
        public string? Description { get; set; }
        public string? CompanyEmail { get; set; }
        public string? CompanyPhone { get; set; }
        public string? CompanyAddress { get; set; }
        //public ICollection<Domains>? Domains { get; set; }
        //public ICollection<CrmContact>? CrmContacts { get; set; }
        //public ICollection<Hosting>? Hostings { get; set; }
    }
}
