using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.CRM.DTOs;
using Erp.Modules.CRM.Entities;
using Erp.Modules.CRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.CRM
{
    public class MotherCompanyRepository : GenericRepository<MotherCompany>, IMotherCompanyRepository
    {
        public MotherCompanyRepository(AppDbContext context) : base(context)
        {
        }
    }
}
