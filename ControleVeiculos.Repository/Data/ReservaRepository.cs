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

                string sql = string.Format("SELECT ISNULL(MAX(CAST(reservasID AS INT))+1,1) FROM dbo.Reservass");
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

                ReservaDapper reservasDapper = reserva.Map(reserva.reservaID);

                conn.Update<ReservaDapper>(reservasDapper);
            }
        }

        public Reserva GetByID(int reservasID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Reservass WHERE reservasID = '{0}'", reservasID);

                return conn.Query<Reserva>(sql).FirstOrDefault();
            }
        }

        public List<Reserva> GetAll(FilterReservaCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT t.reservasID, t.packageName, pv.parameterValue as tecnologyID, " +
                                           "pv1.parameterValue as statusID, pv2.parameterValue as browserID, pv3.parameterValue as methodologyID, " +
                                           "pv4.parameterValue as deviceID,pv5.parameterValue as platformNameID, d.demandCode + ' - ' + d.demandName as demandID " +
                                           "FROM Reservass t " +
                                           "INNER JOIN ParameterValues pv on t.tecnologyID = pv.parameterValueID " +
                                           "INNER JOIN ParameterValues pv1 on t.statusID = pv1.parameterValueID " +
                                           "INNER JOIN ParameterValues pv2 on t.browserID = pv2.parameterValueID " +
                                           "INNER JOIN ParameterValues pv3 on t.methodologyID = pv3.parameterValueID " +
                                           "LEFT JOIN ParameterValues pv4 on t.deviceID = pv4.parameterValueID " +
                                           "LEFT JOIN ParameterValues pv5 on t.platformNameID = pv5.parameterValueID " +
                                           "INNER JOIN Demands d on t.demandID = d.demandID WHERE 1 = 1");

                //if (!string.IsNullOrEmpty(command.TecnologyID))
                //    sql += string.Format("AND t.tecnologyID = '{0}' ", command.TecnologyID);

                //if (!string.IsNullOrEmpty(command.BrowserID))
                //    sql += string.Format("AND t.browserID = '{0}' ", command.BrowserID);

                //if (!string.IsNullOrEmpty(command.StatusID))
                //    sql += string.Format("AND t.statusID = '{0}' ", command.StatusID);

                //if (!string.IsNullOrEmpty(command.DemandID))
                //    sql += string.Format("AND t.demandID = '{0}' ", command.DemandID);

                //sql += "ORDER BY packageName";

                return conn.Query<Reserva>(sql).ToList();
            }
        }

        public void Delete(int reservasID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Reservass WHERE reservasID = '{0}'", reservasID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
