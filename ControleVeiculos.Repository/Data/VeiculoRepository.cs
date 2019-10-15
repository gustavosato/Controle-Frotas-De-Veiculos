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

                string sql = string.Format("SELECT ISNULL(MAX(CAST(logID AS INT))+1,1) FROM dbo.Veiculos");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                VeiculoDapper veiculosDapper = veiculo.Map(primaryKey);

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

        public Veiculo GetByID(int logID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Veiculoss WHERE logID = '{0}'", logID);

                return conn.Query<Veiculo>(sql).FirstOrDefault();
            }
        }

        public List<Veiculo> GetAll(FilterVeiculoCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT logID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM Veiculoss tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                //if (!string.IsNullOrEmpty(command.StatusID))
                //    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY logID";
                return conn.Query<Veiculo>(sql).ToList();
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
