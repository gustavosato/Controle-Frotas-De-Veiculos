using ControleVeiculos.Domain.Entities.Veiculos;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Veiculos;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class VeiculoRepository : BaseRepository, IVeiculoRepository
    {
        public void Add(Veiculo veiculo)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(veiculoID AS INT))+1,1) FROM dbo.Veiculos");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                VeiculoDapper veiculosDapper = veiculo.Map(primaryKey);

                //conn.Insert<VeiculosDapper>(veiculosDapper);

                try
                {
                    conn.Insert<VeiculoDapper>(veiculosDapper);
                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message.ToString());
                    var mensagem = ex.Message;
                }
            }
        }

        public void Update(Veiculo veiculo)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                VeiculoDapper veiculosDapper = veiculo.Map(veiculo.veiculoID);

                conn.Update<VeiculoDapper>(veiculosDapper);
            }
        }

        public Veiculo GetByID(int veiculoID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Veiculos WHERE veiculoID = '{0}'", veiculoID);

                return conn.Query<Veiculo>(sql).FirstOrDefault();
            }
        }

        public List<Veiculo> GetAll(FilterVeiculoCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT v.modelo, v.cor, v.placa, v.status, v.ano, v.motor " +
                                           "FROM Veiculos v " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.Modelo))
                    sql += string.Format("AND v.modelo LIKE '%{0}%' ", command.Modelo);

                if (!string.IsNullOrEmpty(command.Status))
                    sql += string.Format("AND v.status LIKE '%{0}%' ", command.Status);

                if (!string.IsNullOrEmpty(command.Ano))
                    sql += string.Format("AND v.ano LIKE '%{0}%' ", command.Ano);

                if (!string.IsNullOrEmpty(command.Motor))
                    sql += string.Format("AND v.motor LIKE '%{0}%' ", command.Motor);


                sql += "ORDER BY v.modelo";
                return conn.Query<Veiculo>(sql).ToList();
            }
        }

        public void Delete(int veiculoID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Veiculos WHERE veiculoID = '{0}'", veiculoID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
