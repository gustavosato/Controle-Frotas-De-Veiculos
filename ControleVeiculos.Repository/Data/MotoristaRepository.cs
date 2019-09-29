using ControleVeiculos.Domain.Entities.Motoristas;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Motoristas;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class MotoristaRepository : BaseRepository, IMotoristaRepository
    {
        public void Add(Motorista motorista)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(logID AS INT))+1,1) FROM dbo.Motoristas");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                MotoristaDapper motoristaDapper = motorista.Map(primaryKey);

                //conn.Insert<MotoristaDapper>(motoristaDapper);

                try
                {
                    conn.Insert<MotoristaDapper>(motoristaDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Motorista motorista)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                MotoristaDapper motoristaDapper = motorista.Map(motorista.logID);

                conn.Update<MotoristaDapper>(motoristaDapper);
            }
        }

        public Motorista GetByID(int logID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Motoristas WHERE logID = '{0}'", logID);

                return conn.Query<Motorista>(sql).FirstOrDefault();
            }
        }

        public List<Motorista> GetAll(FilterMotoristaCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT logID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM Motoristas tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY logID";
                return conn.Query<Motorista>(sql).ToList();
            }
        }

        public void Delete(int logID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Motoristas WHERE logID = '{0}'", logID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
