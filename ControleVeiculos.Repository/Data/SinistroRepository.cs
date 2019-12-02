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

                string sql = string.Format("SELECT ISNULL(MAX(CAST(sinistroID AS INT))+1,1) FROM dbo.Sinistro");

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

                string sql = string.Format("SELECT * FROM dbo.Sinistro WHERE sinistroID = '{0}'", sinistroID);

                return conn.Query<Sinistro>(sql).FirstOrDefault();
            }
        }

        public List<Sinistro> GetAll(FilterSinistroCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT s.sinistroID, s.apolice, s.franquia, s.tipoSinistro " +
                                           "FROM Sinistro s " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.Apolice))
                    sql += string.Format("AND s.apolice LIKE '%{0}%' ", command.Apolice);

                if (!string.IsNullOrEmpty(command.Franquia))
                    sql += string.Format("AND s.franquia LIKE '%{0}%' ", command.Franquia);

                if (!string.IsNullOrEmpty(command.TipoSinistro))
                    sql += string.Format("AND s.tipoSinistro LIKE '%{0}%' ", command.TipoSinistro);

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

                string sql = string.Format("DELETE FROM dbo.Sinistro WHERE sinistroID = '{0}'", sinistroID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
