using ControleVeiculos.Domain.Entities.Financas;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Financas;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class FinancaRepository : BaseRepository, IFinancaRepository
    {
        public void Add(Financa financa)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(financaID AS INT))+1,1) FROM dbo.Financas");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                FinancaDapper financaDapper = financa.Map(primaryKey);

                //conn.Insert<FinancaDapper>(financaDapper);

                try
                {
                    conn.Insert<FinancaDapper>(financaDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Financa financa)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                FinancaDapper financaDapper = financa.Map(financa.financaID);

                conn.Update<FinancaDapper>(financaDapper);
            }
        }

        public Financa GetByID(int financaID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Financas WHERE financaID = '{0}'", financaID);

                return conn.Query<Financa>(sql).FirstOrDefault();
            }
        }

        public List<Financa> GetAll(FilterFinancaCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT financaID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM Financas tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                //if (!string.IsNullOrEmpty(command.StatusID))
                //    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY financaID";
                return conn.Query<Financa>(sql).ToList();
            }
        }

        public void Delete(int financaID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Financas WHERE financaID = '{0}'", financaID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
