using ControleVeiculos.Domain.Command.Dashboards;
using ControleVeiculos.Domain.Entities.Dashboards;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IDashboardRepository
    {
        List<Dashboard> TimeRelease(FilterDashboardCommand command);
    }
}
