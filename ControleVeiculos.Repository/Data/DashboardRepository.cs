using ControleVeiculos.Domain.Entities.Dashboards;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Dashboards;

namespace ControleVeiculos.Repository.Data
{
    public class DashboardRepository : BaseRepository, IDashboardRepository
    {      
        //time release
        public List<Dashboard> TimeRelease(FilterDashboardCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("select u.userName as item1," +
                                            "CAST(SUM(CASE WHEN t.isApproved = 1 THEN (Convert(decimal,DATEDIFF(MINUTE, t.startWork, t.endWork),114)/60) ELSE 0 END)AS decimal(18, 2)) item2, " +
                                            "CAST(SUM(CASE WHEN t.isApproved = 0 THEN (Convert(decimal,DATEDIFF(MINUTE, t.startWork, t.endWork),114)/60) ELSE 0 END)AS decimal(18, 2)) item3, " +
                                            "replace(cast(Convert(decimal, SUM(DATEDIFF(MINUTE, t.startWork, t.endWork)), 114) / 60 as decimal(18, 2)), '.', ',') as item4 " +
                                            "from TimeReleases t " +
                                            "join Users u on t.createdByID = u.userID " +
                                            "where 1 = 1 ");

                if (!string.IsNullOrEmpty(command.Item1))
                    sql += string.Format("AND Convert(date, t.registerDate, 103) >= Convert(date, '{0}', 103) ", command.Item1);

                if (!string.IsNullOrEmpty(command.Item2))
                    sql += string.Format("AND Convert(date, t.registerDate, 103) <= Convert(date, '{0}', 103) ", command.Item2);

                if (!string.IsNullOrEmpty(command.Item3))
                    sql += string.Format("AND t.createdByID = '{0}' ", command.Item3);

                if (!string.IsNullOrEmpty(command.Item4))
                    sql += string.Format("AND c.customerID = '{0}' ", command.Item4);

                sql += "Group By u.userName Order By 1";

                return conn.Query<Dashboard>(sql).ToList();
            }
        }
    }
}
