using ControleVeiculos.Domain.Entities.Acessorios;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Acessorios;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class AcessorioRepository : BaseRepository, IAcessorioRepository
    {
        public void Add(Acessorio acessorio)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(logID AS INT))+1,1) FROM dbo.Acessorios");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                AcessorioDapper acessorioDapper = acessorio.Map(primaryKey);

                //conn.Insert<AcessorioDapper>(acessorioDapper);

                try
                {
                    conn.Insert<AcessorioDapper>(acessorioDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Acessorio acessorio)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                AcessorioDapper acessorioDapper = acessorio.Map(acessorio.acessorioID);

                conn.Update<AcessorioDapper>(acessorioDapper);
            }
        }

        public Acessorio GetByID(int logID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Acessorios WHERE logID = '{0}'", logID);

                return conn.Query<Acessorio>(sql).FirstOrDefault();
            }
        }

        public List<Acessorio> GetAll(FilterAcessorioCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT logID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM Acessorios tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                //if (!string.IsNullOrEmpty(command.StatusID))
                //    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY logID";
                return conn.Query<Acessorio>(sql).ToList();
            }
        }

        public void Delete(int logID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Acessorios WHERE logID = '{0}'", logID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
