using Erp.Modules.HRM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Data
{
    public class HrmLeaveConfiguration : 
        IEntityTypeConfiguration<LeavePolicy>,
        IEntityTypeConfiguration<LeaveEntitlement>,
        IEntityTypeConfiguration<LeaveApplication>,
        IEntityTypeConfiguration<LeaveApplicationYear>,
        IEntityTypeConfiguration<CompensationGrant>,
        IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<LeavePolicy> builder) 
        {
            builder.ToTable("LeavePolicies", "hrm");
            builder.Property(x => x.LeaveType).HasConversion<int>();
            builder.Property(x => x.RequiredGender).HasConversion<int>();
            builder.HasIndex(x => x.LeaveType).IsUnique();
        }
        public void Configure(EntityTypeBuilder<LeaveEntitlement> builder)
        {
            builder.ToTable("LeaveEntitlements", "hrm");
            builder.Property(x => x.LeaveType).HasConversion<int>();
            builder.Property(x => x.EmployeeType).HasConversion<int>();
            builder.HasOne(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(x => new { x.Year, x.EmployeeId, x.LeaveType }).IsUnique();
        }
        public void Configure(EntityTypeBuilder<LeaveApplication> builder)
        {
            builder.ToTable("LeaveApplications", "hrm");
            builder.Property(x => x.LeaveType).HasConversion<int>();
            builder.Property(x=>x.Status).HasConversion<int>();
            builder.Property(x => x.ManagerComment).HasMaxLength(1000);
            builder.HasOne(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x=>x.DecidedBy).WithMany().HasForeignKey(y => y.DecidedById)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Years).WithOne(x => x.Application).HasForeignKey(x => x.LeaveApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => new { x.EmployeeId, x.LeaveType, x.Status });
        }
        public void Configure(EntityTypeBuilder<LeaveApplicationYear> builder)
        {
            builder.ToTable("LeaveApplicationYears", "hrm");
            builder.HasIndex(x => new {x.Year, x.LeaveApplicationId});
        }
        public void Configure(EntityTypeBuilder<CompensationGrant> builder)
        {
            builder.ToTable("CompensationGrants", "hrm");
            builder.Property(x => x.Reason).HasMaxLength(500);
            builder.HasOne(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.GrantedBy).WithMany().HasForeignKey(x=>x.GrantedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            builder.ToTable("Holidays", "hrm");
            builder.Property(x => x.Name).HasMaxLength(500);
            builder.HasIndex(x => x.Date).IsUnique().HasFilter("[IsDeleted]=0");
        }
    }
}
