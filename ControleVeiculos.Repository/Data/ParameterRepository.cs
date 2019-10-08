using ControleVeiculos.Domain.Entities.Parameters;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Parameters;
using System;
using ControleVeiculos.Repository.Data;

namespace ControleVeiculos.Repository.Data
{
    public class ParameterRepository : BaseRepository, IParameterRepository
    {
        public void Add(Parameter parameter)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(parameterID AS INT))+1,1) FROM dbo.Parameters WHERE systemFeatureID = {0} ", parameter.systemFeatureID);

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                if (primaryKey == 1)

                {
                    string initialChar = parameter.systemFeatureID.Substring(0, 1);
                    primaryKey = Convert.ToInt32(parameter.systemFeatureID.ToString() + initialChar + "00");
                }

                ParameterDapper parameterDapper = parameter.Map(primaryKey);

                conn.Insert<ParameterDapper>(parameterDapper);
            }
        }

        public void Update(Parameter parameter)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                ParameterDapper parameterDapper = parameter.Map(parameter.parameterID);

                conn.Update<ParameterDapper>(parameterDapper);
            }
        }

        public Parameter GetByID(int parameterID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Parameters WHERE parameterID = '{0}'", parameterID);

                return conn.Query<Parameter>(sql).FirstOrDefault();
            }
        }

        public List<Parameter> GetAll(FilterParameterCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT p.parameterID, p.parameterName, s.systemFeatureName as systemfeatureID " +
                                           "FROM Parameters p " +
                                           "INNER JOIN SystemFeatures s on s.systemfeatureID = p.systemfeatureID " +
                                           "Where 1 = 1 ");

                if (!string.IsNullOrEmpty(command.ParameterName))
                    sql += string.Format("AND p.parameterName LIKE '%{0}%' ", command.ParameterName);

                if (!string.IsNullOrEmpty(command.SystemFeatureID))
                    sql += string.Format("AND p.systemFeatureID = '{0}' ", command.SystemFeatureID);

                sql += "ORDER BY parameterID";

                return conn.Query<Parameter>(sql).ToList();
            }
        }

        public List<Parameter> GetAll()
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT p.parameterID, s.SystemFeatureName + ' - ' + p.parameterName as parameterName FROM Parameters p " +
                                           "INNER JOIN SystemFeatures s ON s.systemFeatureID = p.systemFeatureID " +
                                           "Where 1 = 1 " );
                
                sql += "ORDER BY parameterID, parameterName";

                return conn.Query<Parameter>(sql).ToList();
            }
        }

        public void Delete(int parameterID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Parameters WHERE parameterID = '{0}'", parameterID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
