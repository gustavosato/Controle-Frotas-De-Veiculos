using ControleVeiculos.Domain.Entities.PositionsSalaries;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.PositionsSalaries;

namespace ControleVeiculos.Repository.Data
{
    public class PositionsSalarieRepository : BaseRepository, IPositionsSalarieRepository
    {
        public void Add(PositionsSalarie PositionsSalarie)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(positionsSalarieID AS INT))+1,1) FROM dbo.PositionsSalaries");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                PositionsSalarieDapper positionsSalarieDapper = PositionsSalarie.Map(primaryKey);

                conn.Insert<PositionsSalarieDapper>(positionsSalarieDapper);
                                
            }
        }

        public void Update(PositionsSalarie positionsSalarie)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();


                PositionsSalarieDapper positionsSalarieDapper = positionsSalarie.Map(positionsSalarie.positionsSalarieID);

                conn.Update<PositionsSalarieDapper>(positionsSalarieDapper);
            }
        }

        public PositionsSalarie GetByID(int positionsSalarieID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.PositionsSalaries WHERE positionsSalarieID = '{0}'", positionsSalarieID);

                return conn.Query<PositionsSalarie>(sql).FirstOrDefault();
            }
        }

        public List<PositionsSalarie> GetAll(FilterPositionsSalarieCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT positionsSalarieID, pv.parameterValue as functionID, pv1.parameterValue as classificationID, pv2.parameterValue as levelID, " +
                                            "FORMAT(Convert(float, replace(replace(amountPJ, ',', '.'), 'R$', '')), 'c', 'pt-br') as AmountPJ, " +
                                            "FORMAT(Convert(float, replace(replace(amountCLT, ',', '.'), 'R$', '')), 'c', 'pt-br') as AmountCLT, " +
                                            "FORMAT(Convert(float, replace(replace(amountCLTFLEX, ',', '.'), 'R$', '')), 'c', 'pt-br') as AmountCLTFLEX " +
                                            "FROM PositionsSalaries p " +
                                            "INNER JOIN ParameterValues pv on p.functionID = pv.parameterValueID " +
                                            "INNER JOIN ParameterValues pv1 on p.classificationID = pv1.parameterValueID " +
                                            "INNER JOIN ParameterValues pv2 on p.levelID = pv2.parameterValueID " +
                                            "WHERE 1 = 1 ");

                    if (!string.IsNullOrEmpty(command.FunctionID))
                    sql += string.Format("AND functionID = '{0}' ", command.FunctionID);

                sql += "ORDER BY p.functionID";

                return conn.Query<PositionsSalarie>(sql).ToList();
            }
        }

        public void Delete(int positionsSalarieID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.PositionsSalaries WHERE positionsSalarieID = '{0}'", positionsSalarieID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
