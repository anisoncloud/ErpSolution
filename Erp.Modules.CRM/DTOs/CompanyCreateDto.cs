using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.DTOs
{
    public record CompanyCreateDto(
        string Name,
        string Description,
        int MotherCompanyId,
        string CompanyEmail,
        string CompanyPhone,
        string CompanyAddress
        );
    
}
