using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Dashboards;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Dashboards;
using System.Collections.Generic;

namespace Lean.Test.Cloud.ApplicationService
{
    public class DashboardService : BaseAppService, IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        //time release
        public List<Dashboard> TimeRelease(FilterDashboardCommand filterDashboardCommand)
        {
            var dashboard = _dashboardRepository.TimeRelease(filterDashboardCommand);

            return new List<Dashboard>(dashboard);
        }
    }
}

