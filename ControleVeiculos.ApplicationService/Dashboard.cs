using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Dashboards;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Dashboards;
using System.Collections.Generic;

namespace ControleVeiculos.ApplicationService
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

