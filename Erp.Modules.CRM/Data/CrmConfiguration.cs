using Erp.Modules.CRM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.CRM.Data
{
    public class CrmConfiguration :
        IEntityTypeConfiguration<Domains>,
        IEntityTypeConfiguration<MotherCompany>,
        IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Domains> builder) 
        {
            builder.ToTable("Domains", "crm");
        }
        public void Configure(EntityTypeBuilder<MotherCompany> builder) 
        {
            builder.ToTable("MotherCompany", "crm");
        }
        public void Configure(EntityTypeBuilder<Company> builder) 
        {
            builder.ToTable("Company", "crm");
        }

    }
}
