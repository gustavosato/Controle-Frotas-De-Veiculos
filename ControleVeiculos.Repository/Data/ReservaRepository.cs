using ControleVeiculos.Domain.Entities.Reservas;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Reservas;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class ReservaRepository : BaseRepository, IReservaRepository
    {
        public void Add(Reserva reserva)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(reservaID AS INT))+1,1) FROM dbo.Reserva");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                ReservaDapper reservasDapper = reserva.Map(primaryKey);
                try
                {
                    conn.Insert<ReservaDapper>(reservasDapper);
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }

        public void Update(Reserva reserva)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                ReservaDapper reservaDapper = reserva.Map(reserva.reservaID);

                conn.Update<ReservaDapper>(reservaDapper);
            }
        }

        public Reserva GetByID(int reservaID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Reserva WHERE reservaID = '{0}'", reservaID);

                return conn.Query<Reserva>(sql).FirstOrDefault();
            }
        }

        public List<Reserva> GetAll(FilterReservaCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT r.reservaID, r.destino, r.dataReserva, " +
                                           "v.modelo as veiculoID, f.nomeFuncionario as funcionarioID, r.finalidade " +
                                           "FROM Reserva r " +
                                           "INNER JOIN Veiculos v on r.veiculoID = v.veiculoID " +
                                           "INNER JOIN Funcionarios f on r.funcionarioID = f.FuncionarioID " +
                                           "WHERE 1 = 1");

                if (!string.IsNullOrEmpty(command.Destino))
                    sql += string.Format("AND r.destino LIKE '%{0}%' ", command.Destino);

                if (!string.IsNullOrEmpty(command.DataReserva))
                    sql += string.Format("AND r.dataReserva LIKE '%{0}%' ", command.DataReserva);

                if (!string.IsNullOrEmpty(command.FuncionarioID))
                    sql += string.Format("AND t.funcionarioID = '{0}' ", command.FuncionarioID);

                if (!string.IsNullOrEmpty(command.VeiculoID))
                    sql += string.Format("AND t.veiculoID = '{0}' ", command.VeiculoID);

                sql += "ORDER BY reservaID";

                return conn.Query<Reserva>(sql).ToList();
            }
        }

        public void Delete(int reservaID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Reserva WHERE reservaID = '{0}'", reservaID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
