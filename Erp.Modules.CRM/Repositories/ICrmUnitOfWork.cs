using Erp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.Repositories
{
    public interface ICrmUnitOfWork : IUnitOfWork
    {
        IMotherCompanyRepository MotherCompanys { get; }
    }
}
