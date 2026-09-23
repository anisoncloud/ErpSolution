using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.Data
{
    public static class CrmModuleExtensions
    {
        public static void ApplyCrmModule(this ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(CrmModuleExtensions).Assembly);
        }
    }
}
