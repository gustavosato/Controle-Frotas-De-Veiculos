using ControleVeiculos.Domain.Entities.EntradaSaidas;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.EntradaSaidas;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class EntradaSaidaRepository : BaseRepository, IEntradaSaidaRepository
    {
        public void Add(EntradaSaida entradaSaida)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(logID AS INT))+1,1) FROM dbo.EntradaSaidas");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                EntradaSaidaDapper entradaSaidaDapper = entradaSaida.Map(primaryKey);

                //conn.Insert<EntradaSaidaDapper>(entradaSaidaDapper);

                try
                {
                    conn.Insert<EntradaSaidaDapper>(entradaSaidaDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(EntradaSaida entradaSaida)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                EntradaSaidaDapper entradaSaidaDapper = entradaSaida.Map(entradaSaida.logID);

                conn.Update<EntradaSaidaDapper>(entradaSaidaDapper);
            }
        }

        public EntradaSaida GetByID(int logID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.EntradaSaidas WHERE logID = '{0}'", logID);

                return conn.Query<EntradaSaida>(sql).FirstOrDefault();
            }
        }

        public List<EntradaSaida> GetAll(FilterEntradaSaidaCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT logID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM EntradaSaidas tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY logID";
                return conn.Query<EntradaSaida>(sql).ToList();
            }
        }

        public void Delete(int logID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.EntradaSaidas WHERE logID = '{0}'", logID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
