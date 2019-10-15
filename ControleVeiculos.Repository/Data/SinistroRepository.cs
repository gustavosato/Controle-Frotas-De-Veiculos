using ControleVeiculos.Domain.Entities.Sinistros;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Sinistros;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class SinistroRepository : BaseRepository, ISinistroRepository
    {
        public void Add(Sinistro sinistro)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(sinistroID AS INT))+1,1) FROM dbo.Sinistros");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                SinistroDapper sinistroDapper = sinistro.Map(primaryKey);

                //conn.Insert<SinistroDapper>(sinistroDapper);

                try
                {
                    conn.Insert<SinistroDapper>(sinistroDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Sinistro sinistro)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                SinistroDapper sinistroDapper = sinistro.Map(sinistro.sinistroID);

                conn.Update<SinistroDapper>(sinistroDapper);
            }
        }

        public Sinistro GetByID(int sinistroID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Sinistros WHERE sinistroID = '{0}'", sinistroID);

                return conn.Query<Sinistro>(sql).FirstOrDefault();
            }
        }

        public List<Sinistro> GetAll(FilterSinistroCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT sinistroID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM Sinistros tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                //if (!string.IsNullOrEmpty(command.StatusID))
                //    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY sinistroID";
                return conn.Query<Sinistro>(sql).ToList();
            }
        }

        public void Delete(int sinistroID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Sinistros WHERE sinistroID = '{0}'", sinistroID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
