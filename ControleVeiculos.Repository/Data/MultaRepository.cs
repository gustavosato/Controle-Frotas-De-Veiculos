using ControleVeiculos.Domain.Entities.Multas;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Multas;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class MultaRepository : BaseRepository, IMultaRepository
    {
        public void Add(Multa multa)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(multaID AS INT))+1,1) FROM dbo.Multas");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                MultaDapper multaDapper = multa.Map(primaryKey);

                //conn.Insert<MultaDapper>(multaDapper);

                try
                {
                    conn.Insert<MultaDapper>(multaDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Multa multa)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                MultaDapper multaDapper = multa.Map(multa.multaID);

                conn.Update<MultaDapper>(multaDapper);
            }
        }

        public Multa GetByID(int multaID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Multas WHERE multaID = '{0}'", multaID);

                return conn.Query<Multa>(sql).FirstOrDefault();
            }
        }

        public List<Multa> GetAll(FilterMultaCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT m.multaID, m.veiculoID, m.funcionarioID " +
                                           "FROM Multas m " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.VeiculoID))
                    sql += string.Format("AND m.veiculoID LIKE '%{0}%' ", command.VeiculoID);

                sql += "ORDER BY multaID";
                return conn.Query<Multa>(sql).ToList();
            }
        }

        public void Delete(int multaID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Multas WHERE multaID = '{0}'", multaID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
