using Erp.Modules.HRM.Repositories;
using Erp.Modules.HRM.Settings;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public sealed class HrmClock : IHrmClock
    {
        private readonly TimeZoneInfo _tz;
        public HrmClock(HrmSettings settings)
        {
            try
            {
                _tz = TimeZoneInfo.FindSystemTimeZoneById(settings.TimeZoneId);
            }
            catch (TimeZoneNotFoundException)
            {
                _tz = TimeZoneInfo.Utc;
            }            
        }
        public DateTime Now => TimeZoneInfo.ConvertTime(DateTime.UtcNow, _tz);
        public DateOnly Today => DateOnly.FromDateTime(Now);
        
    }
}
