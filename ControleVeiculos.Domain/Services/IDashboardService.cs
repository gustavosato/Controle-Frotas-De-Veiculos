using Lean.Test.Cloud.Domain.Command.Dashboards;
using Lean.Test.Cloud.Domain.Entities.Dashboards;
using System;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IDashboardService : IDisposable
    {
        List<Dashboard> TimeRelease(FilterDashboardCommand command);
    }
}
