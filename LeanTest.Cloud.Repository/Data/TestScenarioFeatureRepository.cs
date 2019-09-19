using Lean.Test.Cloud.Domain.Entities.TestScenarioFeatures;
using Lean.Test.Cloud.Domain.Entities.Features;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.TestScenarioFeatures;
using Lean.Test.Cloud.Domain.Command.Features;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class TestScenarioFeatureRepository : BaseRepository, ITestScenarioFeatureRepository
    {
        public void Add(TestScenarioFeature testScenarioFeature)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(testScenarioFeatureID AS INT))+1,1) FROM dbo.TestScenarioFeatures");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                TestScenarioFeatureDapper testScenarioFeatureDapper = testScenarioFeature.Map(primaryKey);

                conn.Insert<TestScenarioFeatureDapper>(testScenarioFeatureDapper);
            }
        }
       
        public void Delete(int testScenarioFeatureID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.TestScenarioFeatures WHERE testScenarioFeatureID = '{0}' ", testScenarioFeatureID);
                conn.ExecuteScalar(sql);
            }
        }

        public List<TestScenarioFeature> GetAllAssociateTestScenarioByFeatureID(FilterTestScenarioFeatureCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT tf.testScenarioID, tf.testScenarioFeatureID, f.featureName,tf.executionOrder, " +
                                           "tf.isLoop, tf.toolsTestID " +
                                           "FROM TestScenarioFeatures tf " +
                                           "INNER JOIN Features f ON tf.featureID = f.featureID " +
                                           "WHERE tf.testScenarioID = {0} ", command.TestScenarioID);

                if (!string.IsNullOrEmpty(command.FeatureName))
                    sql += string.Format("AND f.featureName LIKE '%{0}%' ", command.FeatureName);

                return conn.Query<TestScenarioFeature>(sql).ToList();
            }
        }
        
        public List<TestScenarioFeature> GetAllNoAssociateTestScenarioByFeatureID(FilterTestScenarioFeatureCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT f.featureID, sa.applicationSystemName + ' - ' + f.featureName AS featureName " +
                                           "FROM Features f " +
                                           "INNER JOIN ApplicationSystems sa ON sa.applicationSystemID = f.applicationSystemID " +
                                           "WHERE sa.customerID = {0} ", command.CustomerID);

                if (!string.IsNullOrEmpty(command.FeatureName))
                    sql += string.Format("AND f.featureName LIKE '%{0}%' ", command.FeatureName);

                sql += "ORDER BY f.featureName";
                return conn.Query<TestScenarioFeature>(sql).ToList();
            }
        }
    }
}
