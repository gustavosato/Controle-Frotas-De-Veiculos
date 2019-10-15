using ControleVeiculos.Domain.Entities.Seguros;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Seguros;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class SeguroRepository : BaseRepository, ISeguroRepository
    {
        public void Add(Seguro seguro)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(seguroID AS INT))+1,1) FROM dbo.Seguros");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                SeguroDapper seguroDapper = seguro.Map(primaryKey);

                //conn.Insert<SeguroDapper>(seguroDapper);

                try
                {
                    conn.Insert<SeguroDapper>(seguroDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Seguro seguro)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                SeguroDapper seguroDapper = seguro.Map(seguro.seguroID);

                conn.Update<SeguroDapper>(seguroDapper);
            }
        }

        public Seguro GetByID(int seguroID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Seguros WHERE seguroID = '{0}'", seguroID);

                return conn.Query<Seguro>(sql).FirstOrDefault();
            }
        }

        public List<Seguro> GetAll(FilterSeguroCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT seguroID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM Seguros tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                //if (!string.IsNullOrEmpty(command.StatusID))
                //    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY seguroID";
                return conn.Query<Seguro>(sql).ToList();
            }
        }

        public void Delete(int seguroID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Seguros WHERE seguroID = '{0}'", seguroID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
