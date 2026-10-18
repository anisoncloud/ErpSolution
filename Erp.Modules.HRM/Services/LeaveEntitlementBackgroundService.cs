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
                    var policies = scope.ServiceProvider.GetRequiredService<ILeavePolicyService>();
                    var leave = scope.ServiceProvider.GetRequiredService<ILeaveService>();

                    var seeded = await policies.EnsureDefaultsAsync();
                    if (seeded>0)
                    {
                        _log.LogInformation("Seeded {Count} default leave policies.", seeded);
                    }
                    var year = _clock.Today.Year;
                    var added = await leave.EnsureEntitlementsForYearAsync(year);
                    if (added>0)
                    {
                        _log.LogInformation("Created {Count} leave entytelment rows for {Year}.", added, year);
                    }
                    var now = _clock.Now;
                    delay = now.Date.AddDays(1).AddMinutes(1) - now;
                    if (delay > TimeSpan.FromHours(6))
                    {
                        delay = TimeSpan.FromHours(6);
                    }
                }
                catch (Exception ex) when(ex is not OperationCanceledException)
                {
                    _log.LogError(ex, "Leave entitlement reset failed; will retry.");
                }
                try
                {
                    await Task.Delay(delay, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}
