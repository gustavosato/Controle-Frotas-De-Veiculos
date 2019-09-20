using ControleVeiculos.Domain.Entities.SystemParameters;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.SystemParameters;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class SystemParameterRepository : BaseRepository, ISystemParameterRepository
    {
        public void Add(Domain.Entities.SystemParameters.SystemParameter systemParameter)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(parameterID AS INT))+1,1) FROM dbo.SystemParameters");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();


                SystemParameterDapper systemParameterDapper = systemParameter.Map(primaryKey);

                try
                {
                    conn.Insert<SystemParameterDapper>(systemParameterDapper);

                }
                catch (Exception)
                {

                }
            }
        }

        public void Update(Domain.Entities.SystemParameters.SystemParameter systemParameter)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                SystemParameterDapper systemParameterDapper = systemParameter.Map(systemParameter.parameterID);

                conn.Update<SystemParameterDapper>(systemParameterDapper);
            }
        }

        public SystemParameter GetByID(int parameterID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.SystemParameters WHERE parameterID = '{0}'", parameterID);

                return conn.Query<SystemParameter>(sql).FirstOrDefault();
            }
        }

        public List<SystemParameter> GetAll(FilterSystemParameterCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT * FROM SystemParameters WHERE 1 =  1 ");

                if (!string.IsNullOrEmpty(command.ParamterName))
                    sql += string.Format("AND paramterName LIKE '%{0}%' ", command.ParamterName);

                sql += "ORDER BY parameterID";

                return conn.Query<SystemParameter>(sql).ToList();
            }
        }

        public List<SystemParameter> GetAll()
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT * FROM SystemParameters WHERE 1 =  1 ");

                sql += "ORDER BY parameterID";

                return conn.Query<SystemParameter>(sql).ToList();
            }
        }

        public void Delete(int parameterID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.SystemParameters WHERE parameterID = '{0}'", parameterID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
