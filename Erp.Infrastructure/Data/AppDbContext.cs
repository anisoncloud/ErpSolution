using Erp.Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Data;

namespace Erp.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
        IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(e => e.ToTable("Users", "identity"));
            builder.Entity<ApplicationRole>(e => e.ToTable("Roles", "identity"));
            builder.Entity<IdentityUserRole<Guid>>(e => e.ToTable("UserRoles", "identity"));
            builder.Entity<IdentityUserClaim<Guid>>(e => e.ToTable("UserClaims", "identity"));
            builder.Entity<IdentityUserLogin<Guid>>(e => e.ToTable("UserLogins", "identity"));
            builder.Entity<IdentityRoleClaim<Guid>>(e => e.ToTable("RoleClaims", "identity"));
            builder.Entity<IdentityUserToken<Guid>>(e => e.ToTable("UserTokens", "identity"));

            // ---- Module entities -> their own schema ----
            
            builder.ApplyHrmModule();
        }
    }    

}
