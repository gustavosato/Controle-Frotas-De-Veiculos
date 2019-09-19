using Lean.Test.Cloud.Domain.Entities.DailyLogs;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.DailyLogs;

namespace Lean.Test.Cloud.Repository.Data
{
    public class DailyLogRepository : BaseRepository, IDailyLogRepository
    {
        public string Add(DailyLog dailyLog)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(dailyLogID AS INT))+1,1) FROM dbo.DailyLogs");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                DailyLogDapper dailyLogDapper = dailyLog.Map(primaryKey);

                conn.Insert<DailyLogDapper>(dailyLogDapper);

                return primaryKey.ToString();
            }
        }

        public void Update(DailyLog dailyLog)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                DailyLogDapper dailyLogDapper = dailyLog.Map(dailyLog.dailyLogID);

                conn.Update<DailyLogDapper>(dailyLogDapper);
            }
        }

        public DailyLog GetByID(int dailyLogID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.DailyLogs WHERE dailyLogID = '{0}'", dailyLogID);

                return conn.Query<DailyLog>(sql).FirstOrDefault();
            }
        }

        public List<DailyLog> GetAll(FilterDailyLogCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT d.dailyLogID, d.isInternal, d.description, dm.demandName as demandID, u.userName as createdByID, d.creationDate " +
                                            "FROM DailyLogs d " +
                                            "INNER JOIN Users u ON d.createdByID = u.userID " +
                                            "INNER JOIN Demands dm on d.demandID = dm.demandID " +
                                            "WHERE dm.customerID = {0} ", command.customerID);

                if (!string.IsNullOrEmpty(command.Description))
                    sql += string.Format("AND d.Description LIKE '%{0}%' ", command.Description);

                if (!string.IsNullOrEmpty(command.DemandID))
                    sql += string.Format("AND d.demandID = '{0}' ", command.DemandID);

                if (!string.IsNullOrEmpty(command.CreatedByID))
                    sql += string.Format("AND d.createdByID = '{0}' ", command.CreatedByID);

                sql += "ORDER BY Convert(datetime, d.creationDate, 103) DESC";


                return conn.Query<DailyLog>(sql).ToList();
            }
        }

        public void Delete(int dailyLogID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.DailyLogs WHERE dailyLogID = '{0}'", dailyLogID);
                conn.ExecuteScalar(sql);
            }
        }
    }
}
