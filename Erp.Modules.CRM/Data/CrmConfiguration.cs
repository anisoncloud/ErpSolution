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
        IEntityTypeConfiguration<MotherCompanyDto>,
        IEntityTypeConfiguration<Company>,
        IEntityTypeConfiguration<Contact>,
        IEntityTypeConfiguration<Hosting>
    {
        public void Configure(EntityTypeBuilder<Domains> builder) 
        {
            builder.ToTable("Domains", "crm");
        }
        public void Configure(EntityTypeBuilder<MotherCompanyDto> builder) 
        {
            builder.ToTable("MotherCompany", "crm");
        }
        public void Configure(EntityTypeBuilder<Company> builder) 
        {
            builder.ToTable("Company", "crm");
        }
        public void Configure(EntityTypeBuilder<Hosting> builder) 
        {
            builder.ToTable("Hosting", "crm");
        }
        public void Configure(EntityTypeBuilder<Contact> builder) 
        {
            builder.ToTable("Contact", "crm");
        }

    }
}
