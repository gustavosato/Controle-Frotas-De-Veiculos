using Lean.Test.Cloud.Domain.Entities.TestCases;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.TestCases;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class TestCaseRepository : BaseRepository, ITestCaseRepository
    {
        public void Add(TestCase testCase)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(testCaseID AS INT))+1,1) FROM dbo.TestCases");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                TestCaseDapper testCaseDapper = testCase.Map(primaryKey);

                try
                {
                    conn.Insert<TestCaseDapper>(testCaseDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(TestCase testCase)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                TestCaseDapper testCaseDapper = testCase.Map(testCase.testCaseID);

                conn.Update<TestCaseDapper>(testCaseDapper);
            }
        }

        public TestCase GetByID(int testCaseID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.TestCases WHERE testCaseID = '{0}'", testCaseID);

                return conn.Query<TestCase>(sql).FirstOrDefault();
            }
        }

        public List<TestCase> GetAll(FilterTestCaseCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT tc.testCaseID, tc.testCase, tc.startExecution, tc.endExecution, tc.timeExecution, pv.parameterValue AS statusID, " +
                                           "pv1.parameterValue AS testTypeID, sf.systemFeatureName AS featureID " +
                                           "FROM TestCases tc " +
                                           "INNER JOIN ParameterValues pv ON tc.statusID = pv.parameterValueID " +
                                           "INNER JOIN ParameterValues pv1 ON tc.testTypeID = pv1.parameterValueID " +
                                           "INNER JOIN SystemFeatures sf ON tc.featureID = sf.systemFeatureID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.TestCase))
                    sql += string.Format("AND tc.testCase LIKE '%{0}%' ", command.TestCase);

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND tc.statusID = '{0}' ", command.StatusID);

                if (!string.IsNullOrEmpty(command.TestTypeID))
                    sql += string.Format("AND tc.testTypeID = '{0}' ", command.TestTypeID);

                if (!string.IsNullOrEmpty(command.FeatureID))
                    sql += string.Format("AND tc.featureID = '{0}' ", command.FeatureID);

                sql += "ORDER BY testCaseID";
                return conn.Query<TestCase>(sql).ToList();
            }
        }

        public void Delete(int testCaseID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.TestCases WHERE testCaseID = '{0}'", testCaseID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
