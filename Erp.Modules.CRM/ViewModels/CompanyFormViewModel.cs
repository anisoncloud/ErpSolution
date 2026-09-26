using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.ViewModels
{
    public class CompanyFormViewModel
    {
        public int MotherCompanyId { get; set; }
        public string Name {  get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? CompanyEmail { get; set; }
        public string? CompanyPhone { get; set; }
        public string? CompanyAddress { get; set; }
    }
}
