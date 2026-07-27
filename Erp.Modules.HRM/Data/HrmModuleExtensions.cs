using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Data
{
    public static class HrmModuleExtensions
    {
        /// <summary>
        /// HRM owns its own EF configuration. AppDbContext calls this ONE line
        /// instead of configuring HRM tables itself — keeps modules from bleeding
        /// into each other's setup.
        /// </summary>
        /// 
        public static void ApplyHrmModule(this ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(HrmModuleExtensions).Assembly);
        }
    }
}
