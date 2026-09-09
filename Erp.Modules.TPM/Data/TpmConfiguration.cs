using Erp.Modules.TPM.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.Data
{
    public class TpmConfiguration : 
        IEntityTypeConfiguration<ProjectItem>,
        IEntityTypeConfiguration<ProjectTask>,
        IEntityTypeConfiguration<TaskRevision>
    {
        public void Configure(EntityTypeBuilder<ProjectItem> builder)
        {
            builder.ToTable("ProjectItems", "tpm");
        }
        public void Configure(EntityTypeBuilder<TaskRevision> builder)
        {
            builder.ToTable("TaskRevisions", "tpm");
        }
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            builder.ToTable("ProjectTasks", "tpm");

            // One Task has Many Revisions
            builder.HasOne(t=>t.ProjectItem)
                   .WithMany(t => t.ProjectTasks)
                   .HasForeignKey(r => r.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade); // If a task is deleted, delete its history
        }
    }
}
