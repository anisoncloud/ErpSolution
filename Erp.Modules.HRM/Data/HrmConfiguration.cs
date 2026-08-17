using Erp.Modules.HRM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Data
{
    public class HrmConfiguration : 
        IEntityTypeConfiguration<Department>,
        IEntityTypeConfiguration<Designation>,
        IEntityTypeConfiguration<Employee>,
        IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments", "hrm");            
        }
        public void Configure(EntityTypeBuilder<Designation> builder)
        {
            builder.ToTable("Designations", "hrm");
        }
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies", "hrm");
            builder.HasMany(p => p.Employees)
                .WithOne(p=>p.Company)
                .HasForeignKey(p=>p.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public void Configure(EntityTypeBuilder<Employee> builder) 
        {
            builder.ToTable("Employees", "hrm");
            builder.HasOne(e=>e.Department)
                .WithMany(d=>d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Designation)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DesignationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
