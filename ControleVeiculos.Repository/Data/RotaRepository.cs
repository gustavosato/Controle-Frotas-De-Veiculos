using ControleVeiculos.Domain.Entities.Rotas;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Rotas;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class RotaRepository : BaseRepository, IRotaRepository
    {
        public void Add(Rota rota)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(rotaID AS INT))+1,1) FROM dbo.Rotas");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                RotaDapper rotaDapper = rota.Map(primaryKey);

                //conn.Insert<RotaDapper>(rotaDapper);

                try
                {
                    conn.Insert<RotaDapper>(rotaDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Rota rota)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                RotaDapper rotaDapper = rota.Map(rota.rotaID);

                conn.Update<RotaDapper>(rotaDapper);
            }
        }

        public Rota GetByID(int rotaID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Rotas WHERE rotaID = '{0}'", rotaID);

                return conn.Query<Rota>(sql).FirstOrDefault();
            }
        }

        public List<Rota> GetAll(FilterRotaCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT r.rotaID, r.cidade, r.estado, r.dataIda, r.DataVolta " +
                                           "FROM Rotas r " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.Cidade))
                    sql += string.Format("AND r.cidade LIKE '%{0}%' ", command.Cidade);

                if (!string.IsNullOrEmpty(command.Estado))
                    sql += string.Format("AND r.estado LIKE '%{0}%' ", command.Estado);

                if (!string.IsNullOrEmpty(command.DataIda))
                    sql += string.Format("AND r.dataIda LIKE '%{0}%' ", command.DataIda);

                if (!string.IsNullOrEmpty(command.DataVolta))
                    sql += string.Format("AND r.dataVolta LIKE '%{0}%' ", command.DataVolta);

                sql += "ORDER BY rotaID";
                return conn.Query<Rota>(sql).ToList();
            }
        }

        public void Delete(int rotaID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Rotas WHERE rotaID = '{0}'", rotaID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
