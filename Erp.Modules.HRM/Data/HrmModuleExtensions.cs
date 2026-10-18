using Erp.Modules.HRM.Services;
using Erp.Modules.HRM.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Data
{
    public static class HrmModuleExtensions
    {
        /// <summary>
        /// HRM owns its own EF configuration. AppDbContext calls this ONE line
        /// instead of configuring HRM tables itself — keeps modules from bleeding
        /// into each other's setup.
        /// </summary>
        /// 
        public static void ApplyHrmModule(this ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(HrmModuleExtensions).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(HrmLeaveConfiguration).Assembly);
            /*builder.ApplyConfigurationsFromAssembly(typeof(HrmLeaveConfiguration).Assembly,
            t => t == typeof(HrmLeaveConfiguration) || t == typeof(EmployeeLeaveLinkConfiguration));*/
        }
        /// <summary>Registers the Leave feature (and later Attendance / Late In). Existing HRM registrations
        /// (IEmployeeService, IDepartmentService, IHrmUnitOfWork, ...) are left where they are.</summary>
        public static IServiceCollection AddHrmModule(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new HrmSettings();
            configuration.GetSection(HrmSettings.Section).Bind(settings);
            services.AddSingleton(settings);

            services.AddSingleton<IHrmClock, HrmClock>();
            services.AddSingleton<ILeaveDayCalculator, LeaveDayCalculator>();

            // Repositories are not registered here: they are created by UnitOfWork and reached through IHrmUnitOfWork.
            services.AddScoped<ILeaveService, LeaveService>();
            services.AddScoped<ILeavePolicyService, LeavePolicyService>();
            services.AddScoped<IHolidayService, HolidayService>();

            services.AddHostedService<LeaveEntitlementBackgroundService>();
            return services;
        }
    }
}
