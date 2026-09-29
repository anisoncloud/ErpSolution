using Erp.Modules.CRM.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.CRM.DTOs
{
    public record DomainDto(
        int Id,
        string Name,
        string NormalizedName,
        string? IpAddress,
        string? Hosting,
        string? DomainRegistrant,
        [property: DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        DateTime? RegistarDate,
        [property:DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        DateTime? LastUpdated,
        int? ForYear,
        [property:DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        DateTime? ExpireDate,
        string? Dns,
        string? Analytics,
        int? CompanyId,        
        Company? Company,
        string? Comments
        );
    
}
