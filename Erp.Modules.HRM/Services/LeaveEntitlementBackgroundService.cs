using Erp.Modules.HRM.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public sealed class LeaveEntitlementBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scops;
        private readonly IHrmClock _clock;
        private readonly ILogger<LeaveEntitlementBackgroundService> _log;

        public LeaveEntitlementBackgroundService(IServiceScopeFactory scops, IHrmClock clock, ILogger<LeaveEntitlementBackgroundService> log)
        {
            _scops = scops;
            _clock = clock;
            _log = log;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var delay = TimeSpan.FromMinutes(5); // retry soon after a failure (e.g. migration not applied yet)
                try
                {
                    using var scope = _scops.CreateScope();
                    //var policies = scope.ServiceProvider.GetRequiredService<ILeavePolicySer>
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
