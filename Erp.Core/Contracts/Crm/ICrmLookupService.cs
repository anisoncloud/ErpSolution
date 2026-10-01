using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Core.Contracts.Crm
{
    public interface ICrmLookupService
    {
        Task<IEnumerable<DomainLookupDto>> GetCrmDomainAsync();
    }
}
