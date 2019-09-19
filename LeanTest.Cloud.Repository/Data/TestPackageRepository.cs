using Lean.Test.Cloud.Domain.Entities.TestPackages;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.TestPackages;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class TestPackageRepository : BaseRepository, ITestPackageRepository
    {
        public void Add(TestPackage testPackage)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(testPackageID AS INT))+1,1) FROM dbo.TestPackages");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                TestPackageDapper testPackageDapper = testPackage.Map(primaryKey);
                try
                {
                    conn.Insert<TestPackageDapper>(testPackageDapper);
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }

        public void Update(TestPackage testPackage)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                TestPackageDapper testPackageDapper = testPackage.Map(testPackage.testPackageID);

                conn.Update<TestPackageDapper>(testPackageDapper);
            }
        }

        public TestPackage GetByID(int testPackageID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.TestPackages WHERE testPackageID = '{0}'", testPackageID);

                return conn.Query<TestPackage>(sql).FirstOrDefault();
            }
        }

        public List<TestPackage> GetAll(FilterTestPackageCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT t.testPackageID, t.packageName, pv.parameterValue as tecnologyID, " +
                                           "pv1.parameterValue as statusID, pv2.parameterValue as browserID, pv3.parameterValue as methodologyID, " +
                                           "pv4.parameterValue as deviceID,pv5.parameterValue as platformNameID, d.demandCode + ' - ' + d.demandName as demandID " +
                                           "FROM TestPackages t " +
                                           "INNER JOIN ParameterValues pv on t.tecnologyID = pv.parameterValueID " +
                                           "INNER JOIN ParameterValues pv1 on t.statusID = pv1.parameterValueID " +
                                           "INNER JOIN ParameterValues pv2 on t.browserID = pv2.parameterValueID " +
                                           "INNER JOIN ParameterValues pv3 on t.methodologyID = pv3.parameterValueID " +
                                           "LEFT JOIN ParameterValues pv4 on t.deviceID = pv4.parameterValueID " +
                                           "LEFT JOIN ParameterValues pv5 on t.platformNameID = pv5.parameterValueID " +
                                           "INNER JOIN Demands d on t.demandID = d.demandID WHERE 1 = 1");

                if (!string.IsNullOrEmpty(command.TecnologyID))
                    sql += string.Format("AND t.tecnologyID = '{0}' ", command.TecnologyID);

                if (!string.IsNullOrEmpty(command.BrowserID))
                    sql += string.Format("AND t.browserID = '{0}' ", command.BrowserID);

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND t.statusID = '{0}' ", command.StatusID);

                if (!string.IsNullOrEmpty(command.DemandID))
                    sql += string.Format("AND t.demandID = '{0}' ", command.DemandID);

                sql += "ORDER BY packageName";

                return conn.Query<TestPackage>(sql).ToList();
            }
        }

        public void Delete(int testPackageID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.TestPackages WHERE testPackageID = '{0}'", testPackageID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
