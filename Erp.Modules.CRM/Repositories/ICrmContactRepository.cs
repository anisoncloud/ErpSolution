using Erp.Core.Interfaces;
using Erp.Modules.CRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.Repositories
{
    public interface ICrmContactRepository : IGenericRepository<Contact>
    {
        Task<IEnumerable<Contact>> GetAllContactWithCompany();
    }
}
