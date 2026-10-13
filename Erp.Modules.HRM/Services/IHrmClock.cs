using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public interface IHrmClock
    {
        DateTime Now { get; }
        DateOnly Today { get; }
    }
}
