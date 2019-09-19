using Lean.Test.Cloud.Domain.Entities.ParameterValues;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.ParameterValues;

namespace Lean.Test.Cloud.Repository.Data
{
    public class ParameterValueRepository : BaseRepository, IParameterValueRepository
    {
        public void Add(ParameterValue parameterValue)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(parameterValueID AS INT))+1,1) FROM dbo.ParameterValues WHERE ParameterID = " + parameterValue.parameterID);
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                if (primaryKey == 1)
                {
                    string initialChar = parameterValue.parameterID.Substring(0, 1);

                    primaryKey = Convert.ToInt32(parameterValue.parameterID + initialChar + "00");
                }

                ParameterValueDapper parameterValueDapper = parameterValue.Map(primaryKey);

                try
                {
                    conn.Insert<ParameterValueDapper>(parameterValueDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(ParameterValue parameterValue)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                ParameterValueDapper parameterValueDapper = parameterValue.Map(parameterValue.parameterValueID);

                conn.Update<ParameterValueDapper>(parameterValueDapper);
            }
        }

        public ParameterValue GetByID(int parameterValueID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.ParameterValues WHERE parameterValueID = '{0}'", parameterValueID);

                return conn.Query<ParameterValue>(sql).FirstOrDefault();
            }
        }

        public List<ParameterValue> GetAll(FilterParameterValueCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("select p.parameterName as parameterID, v.parameterValueID, v.parameterValue, v.parentID, v.isSystem " +
                                           "from ParameterValues v " +
                                           "INNER JOIN Parameters p ON v.parameterID = p.parameterID " +
                                           "Where 1 = 1 ");

                if (!string.IsNullOrEmpty(command.ParameterValue))
                    sql += string.Format("AND v.parameterValue LIKE '%{0}%' ", command.ParameterValue);

                if (!string.IsNullOrEmpty(command.ParameterID))
                    sql += string.Format("AND v.parameterID = '{0}' ", command.ParameterID);

                sql += "ORDER BY v.parameterID, v.parentID";

                return conn.Query<ParameterValue>(sql).ToList();
            }
        }

        public List<ParameterValue> GetAllByParameterID(string parameterID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT * FROM ParameterValues WHERE parameterID = '{0}' ORDER BY parentID, parameterValue", parameterID);

                return conn.Query<ParameterValue>(sql).ToList();    
            }
        }

        public void Delete(int parameterValueID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.ParameterValues WHERE parameterValueID = '{0}'", parameterValueID);

                conn.ExecuteScalar(sql);
            }
        }

        public string GetParameterValueByID(int parameterValueID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT parameterValue FROM dbo.ParameterValues WHERE parameterValueID = " + parameterValueID);

                return conn.Query<string>(sql).FirstOrDefault();
                
            }
        }
    }
}
