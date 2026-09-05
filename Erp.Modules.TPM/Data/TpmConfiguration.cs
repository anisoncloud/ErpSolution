using Erp.Modules.TPM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.Data
{
    public class TpmConfiguration : 
        IEntityTypeConfiguration<Project>, 
        IEntityTypeConfiguration<ProjectTask>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects", "project");
        }
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            builder.ToTable("ProjectTasks", "project");

            // One Task has Many Revisions
            builder.HasMany(t => t.Revisions)
                   .WithOne(r => r.ProjectTask)
                   .HasForeignKey(r => r.ProjectTaskId)
                   .OnDelete(DeleteBehavior.Cascade); // If a task is deleted, delete its history
        }
    }
}
