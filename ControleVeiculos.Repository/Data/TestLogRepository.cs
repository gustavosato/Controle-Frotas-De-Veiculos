using ControleVeiculos.Domain.Entities.TestLogs;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.TestLogs;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class TestLogRepository : BaseRepository, ITestLogRepository
    {
        public void Add(TestLog testLog)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(logID AS INT))+1,1) FROM dbo.TestLogs");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                TestLogDapper testLogDapper = testLog.Map(primaryKey);

                //conn.Insert<TestLogDapper>(testLogDapper);

                try
                {
                    conn.Insert<TestLogDapper>(testLogDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(TestLog testLog)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                TestLogDapper testLogDapper = testLog.Map(testLog.logID);

                conn.Update<TestLogDapper>(testLogDapper);
            }
        }

        public TestLog GetByID(int logID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.TestLogs WHERE logID = '{0}'", logID);

                return conn.Query<TestLog>(sql).FirstOrDefault();
            }
        }

        public List<TestLog> GetAll(FilterTestLogCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT logID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM TestLogs tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY logID";
                return conn.Query<TestLog>(sql).ToList();
            }
        }

        public void Delete(int logID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.TestLogs WHERE logID = '{0}'", logID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
