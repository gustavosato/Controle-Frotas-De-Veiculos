using Lean.Test.Cloud.Domain.Command.Dashboards;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Dashboards
{
    public static class DashboardExtensions
    {
        public static Result<Dashboard> GetDashboard(this Dashboard dashboard)
        {
            return Result.Ok(0, "", dashboard);
        }

        public static Dashboard Map(this Dashboard dashboard, MaintenanceDashboardCommand command)
        {
            dashboard.dashboardID = command.DashboardID;
            dashboard.item1 = command.Item1;
            dashboard.item2 = command.Item2;
            dashboard.item3 = command.Item3;
            dashboard.item4 = command.Item4;
            dashboard.item5 = command.Item5;
            dashboard.item6 = command.Item6;
            dashboard.item7 = command.Item7;
            dashboard.item8 = command.Item8;
            dashboard.item9 = command.Item9;
            dashboard.item10 = command.Item10;
            dashboard.item11 = command.Item11;
            dashboard.item12 = command.Item12;
            dashboard.item13 = command.Item13;
            dashboard.item14 = command.Item14;
            dashboard.item15 = command.Item15;
            dashboard.item16 = command.Item16;
            dashboard.item17 = command.Item17;
            dashboard.item18 = command.Item18;
            dashboard.item19 = command.Item19;
            dashboard.item20 = command.Item20;
            dashboard.createdByID = command.CreatedByID;
            dashboard.creationDate = command.CreationDate;
            dashboard.modifiedByID = command.ModifiedByID;
            dashboard.lastModifiedDate = command.LastModifiedDate;

            return dashboard;
        }
    }
}
