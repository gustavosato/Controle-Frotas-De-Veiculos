using ControleVeiculos.Domain.Entities.SystemFeatures;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.SystemFeatures;

namespace ControleVeiculos.Repository.Data
{
    public class SystemFeatureRepository : BaseRepository, ISystemFeatureRepository
    {
        public void Add(SystemFeature systemFeature)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                var typeID = systemFeature.systemFeatureTypeID.Substring(8,1);

                string sql = string.Format("SELECT ISNULL(MAX(CAST(systemFeatureID AS INT))+1,1) FROM dbo.SystemFeatures Where systemFeatureTypeID LIKE '%" + typeID + "'");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                SystemFeatureDapper systemFeatureDapper = systemFeature.Map(primaryKey);

                conn.Insert<SystemFeatureDapper>(systemFeatureDapper);
            }
        }

        public void Update(SystemFeature systemFeature)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                SystemFeatureDapper systemFeatureDapper = systemFeature.Map(systemFeature.systemFeatureID);

                conn.Update<SystemFeatureDapper>(systemFeatureDapper);
            }
        }

        public SystemFeature GetByID(int systemFeatureID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.SystemFeatures WHERE systemFeatureID = '{0}'", systemFeatureID);

                return conn.Query<SystemFeature>(sql).FirstOrDefault();
            }
        }

        public List<SystemFeature> GetAll(FilterSystemFeatureCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT systemFeatureID, systemFeatureName, v.parameterValue as systemFeatureTypeID " +
                                            "FROM SystemFeatures s " +
                                            "INNER JOIN ParameterValues v ON  s.systemFeatureTypeID = v.parameterValueID " +
                                            "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.SystemFeatureName))
                    sql += string.Format("AND systemFeatureName LIKE '%{0}%' ", command.SystemFeatureName);

                if (!string.IsNullOrEmpty(command.SystemFeatureTypeID))
                    sql += string.Format("AND systemFeatureTypeID = '{0}' ", command.SystemFeatureTypeID);

                sql += "ORDER BY systemFeatureID";

                return conn.Query<SystemFeature>(sql).ToList();
            }
        }

        public List<SystemFeature> GetAll()
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT * FROM SystemFeatures " +
                                            "WHERE 1 = 1 ");
                                
                sql += "ORDER BY systemFeatureName, systemFeatureTypeID";

                return conn.Query<SystemFeature>(sql).ToList();
            }
        }

        public void Delete(int systemFeatureID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.SystemFeatures WHERE systemFeatureID = '{0}'", systemFeatureID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
