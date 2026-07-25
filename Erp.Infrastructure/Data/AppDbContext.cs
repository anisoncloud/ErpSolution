using Erp.Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
        IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        
        }


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
            // Example (add these once module entities exist):
            // builder.Entity<Employee>().ToTable("Employees", "hrm");
            // builder.Entity<Invoice>().ToTable("Invoices", "accounting");
            // builder.Entity<Product>().ToTable("Products", "inventory");
        }
    }    

}
