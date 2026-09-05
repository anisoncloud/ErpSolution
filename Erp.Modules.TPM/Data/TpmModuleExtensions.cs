using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.Data
{
    public static class TpmModuleExtensions
    {
        public static void ApplyTpmModule(this ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(TpmModuleExtensions).Assembly);
        }
    }
}
