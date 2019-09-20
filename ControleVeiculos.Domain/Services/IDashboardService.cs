using ControleVeiculos.Domain.Command.Dashboards;
using ControleVeiculos.Domain.Entities.Dashboards;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface IDashboardService : IDisposable
    {
        List<Dashboard> TimeRelease(FilterDashboardCommand command);
    }
}
