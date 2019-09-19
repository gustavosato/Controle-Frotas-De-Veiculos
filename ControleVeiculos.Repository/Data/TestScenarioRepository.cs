using Lean.Test.Cloud.Domain.Entities.TestScenarios;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.TestScenarios;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class TestScenarioRepository : BaseRepository, ITestScenarioRepository
    {
        public void Add(TestScenario testScenario)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(testScenarioID AS INT))+1,1) FROM dbo.TestScenarios");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                TestScenarioDapper testScenarioDapper = testScenario.Map(primaryKey);

                conn.Insert<TestScenarioDapper>(testScenarioDapper);
               
            }
        }

        public void Update(TestScenario testScenario)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                TestScenarioDapper testScenarioDapper = testScenario.Map(testScenario.testScenarioID);

                conn.Update<TestScenarioDapper>(testScenarioDapper);
            }
        }

        public TestScenario GetByID(int testScenarioID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.TestScenarios WHERE testScenarioID = '{0}'", testScenarioID);

                return conn.Query<TestScenario>(sql).FirstOrDefault();
            }
        }

        public List<TestScenario> GetAll(FilterTestScenarioCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ts.testScenarioID, ts.testScenario, pv.parameterValue AS testTypeID, pv1.parameterValue AS executionTypeID, pv2.parameterValue AS statusID, " +
                                           "ts.startExecution, ts.endExecution, ts.timeExecution " +
                                           "FROM TestScenarios ts " +
                                           "INNER JOIN ParameterValues pv ON ts.testTypeID = pv.parameterValueID " +
                                           "INNER JOIN ParameterValues pv1 ON ts.executionTypeID = pv1.parameterValueID " +
                                           "INNER JOIN ParameterValues pv2 ON ts.statusID = pv2.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND ts.statusID = '{0}' ", command.StatusID);

                if (!string.IsNullOrEmpty(command.TestTypeID))
                    sql += string.Format("AND ts.testTypeID = '{0}' ", command.TestTypeID);

                if (!string.IsNullOrEmpty(command.ExecutionTypeID))
                    sql += string.Format("AND ts.executionTypeID = '{0}' ", command.ExecutionTypeID);

                if (!string.IsNullOrEmpty(command.TestScenario))
                    sql += string.Format("AND ts.testScenario LIKE '%{0}%' ", command.TestScenario);

                sql += "ORDER BY testScenarioID";
                return conn.Query<TestScenario>(sql).ToList();
            }
        }

        public void Delete(int testScenarioID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.TestScenarios WHERE testScenarioID = '{0}'", testScenarioID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
