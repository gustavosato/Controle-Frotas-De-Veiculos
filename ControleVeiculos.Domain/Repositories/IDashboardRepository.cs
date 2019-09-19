using Lean.Test.Cloud.Domain.Command.Dashboards;
using Lean.Test.Cloud.Domain.Entities.Dashboards;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IDashboardRepository
    {
        List<Dashboard> TimeRelease(FilterDashboardCommand command);
    }
}
