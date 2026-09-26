using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.DTOs
{
    public record CompanyDto(
        int Id,
        string Name,
        string Description,        
        string CompanyEmail,
        string CompanyPhone,
        string CompanyAddress
        );
    
}
