using ControleVeiculos.Domain.Entities.Abastecimentos;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Abastecimentos;

namespace ControleVeiculos.Repository.Data
{
    public class AbastecimentoRepository : BaseRepository, IAbastecimentoRepository
    {
        public void Add(Abastecimento abastecimento)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(abastecimentoID AS INT))+1,1) FROM dbo.Abastecimentos");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                AbastecimentoDapper abastecimentoDapper = abastecimento.Map(primaryKey);

                conn.Insert<AbastecimentoDapper>(abastecimentoDapper);
            }
        }

        public void Update(Abastecimento abastecimento)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                AbastecimentoDapper abastecimentoDapper = abastecimento.Map(abastecimento.abastecimentoID);

                conn.Update<Abastecimento>(abastecimento);
            }
        }

        public Abastecimento GetByID(int abastecimentoID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Abastecimentos WHERE abastecimentoID = '{0}'", abastecimentoID);

                return conn.Query<Abastecimento>(sql).FirstOrDefault();
            }
        }

        public List<Abastecimento> GetAll(FilterAbastecimentoCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT * FROM Abastecimentos WHERE 1 =  1 ");

                //if (!string.IsNullOrEmpty(command.AbastecimentoName))
                //    sql += string.Format("AND abastecimentoName LIKE '%{0}%' ", command.AbastecimentoName);

                sql += "ORDER BY abastecimentoID";
                return conn.Query<Abastecimento>(sql).ToList();
            }
        }

        public void Delete(int abastecimentoID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Abastecimentos WHERE abastecimentoID = '{0}'", abastecimentoID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
