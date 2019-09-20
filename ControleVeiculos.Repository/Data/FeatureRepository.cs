using ControleVeiculos.Domain.Entities.Features;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Features;

namespace ControleVeiculos.Repository.Data
{
    public class FeatureRepository : BaseRepository, IFeatureRepository
    {
        public void Add(Feature feature)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(featureID AS INT))+1,1) FROM dbo.Features");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                FeatureDapper featureDapper = feature.Map(primaryKey);

                conn.Insert<FeatureDapper>(featureDapper);
            }
        }

        public void Update(Feature feature)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                FeatureDapper featureDapper = feature.Map(feature.featureID);

                conn.Update<FeatureDapper>(featureDapper);
            }
        }

        public string GetFeatureNameByID(int featureID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT featureName FROM dbo.Features WHERE featureID = {0}", featureID);

                return conn.Query<string>(sql).FirstOrDefault();
            }
        }



        public Feature GetByID(int featureID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Features WHERE featureID = '{0}'", featureID);

                return conn.Query<Feature>(sql).FirstOrDefault();
            }
        }

        public List<Feature> GetAll(int userID, FilterFeatureCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT  f.featureID, c.customerName as customerID, a.applicationSystemName as applicationSystemID, f.featureName, v.parameterValue as featureTypeID, " +
                    "v1.parameterValue as statusID, u.userName as developerID, f.targetDate FROM Features f INNER JOIN ApplicationSystems a ON a.applicationSystemID = f.applicationSystemID " +
                    "INNER JOIN Customers c on a.customerID = c.customerID INNER JOIN CustomersUsers uc on c.customerID = uc.customerID " +
                    "INNER JOIN ParameterValues v on f.featureTypeID = v.parameterValueID " +
                    "INNER JOIN ParameterValues v1 on f.statusID = v1.parameterValueID " +
                    "INNER JOIN Users u on f.developerID = u.userID WHERE uc.userID = {0} ", userID);

                if (!string.IsNullOrEmpty(command.FeatureName))
                    sql += string.Format("AND f.featureName ='{0}'", command.FeatureName);

                if (!string.IsNullOrEmpty(command.ApplicationSystemID))
                    sql += string.Format("AND f.applicationSystemID ='{0}' ", command.ApplicationSystemID);

                sql += "ORDER BY featureID";
                return conn.Query<Feature>(sql).ToList();
            }
        }

        public void Delete(int FeatureID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM Features WHERE featureID = '{0}'", FeatureID);
                conn.ExecuteScalar(sql);
            }
        }

        public List<Feature> GetAll(string applicationSystemID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("select f.featureID, f.featureName " +
                                            "from Features f " +
                                            "Where f.applicationSystemID =  '{0}' ", applicationSystemID);

                sql += "ORDER BY f.featureName";

                return conn.Query<Feature>(sql).ToList();
            }
        }

    }
}
