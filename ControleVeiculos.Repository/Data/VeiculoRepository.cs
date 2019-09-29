using ControleVeiculos.Domain.Entities.Veiculoss;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Veiculoss;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class VeiculoRepository : BaseRepository, IVeiculosRepository
    {
        public void Add(Veiculos veiculos)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(logID AS INT))+1,1) FROM dbo.Veiculoss");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                VeiculoDapper veiculosDapper = veiculos.Map(primaryKey);

                //conn.Insert<VeiculosDapper>(veiculosDapper);

                try
                {
                    conn.Insert<VeiculoDapper>(veiculosDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Veiculos veiculos)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                VeiculoDapper veiculosDapper = veiculos.Map(veiculos.logID);

                conn.Update<VeiculoDapper>(veiculosDapper);
            }
        }

        public Veiculos GetByID(int logID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Veiculoss WHERE logID = '{0}'", logID);

                return conn.Query<Veiculos>(sql).FirstOrDefault();
            }
        }

        public List<Veiculos> GetAll(FilterVeiculosCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT logID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM Veiculoss tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY logID";
                return conn.Query<Veiculos>(sql).ToList();
            }
        }

        public void Delete(int logID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Veiculoss WHERE logID = '{0}'", logID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
