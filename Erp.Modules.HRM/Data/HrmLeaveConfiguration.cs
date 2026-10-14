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
            builder.Property(x=>x.RequiredGender).HasConversion<int>();
            builder.HasIndex(x => x.LeaveType).IsUnique();
        }
        public void Configure(EntityTypeBuilder<LeaveEntitlement> builder)
        {
            builder.ToTable("LeaveEntitlements", "hrm");
        }
    }
}
