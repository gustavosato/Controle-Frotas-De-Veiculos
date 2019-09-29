using ControleVeiculos.Domain.Entities.Manutencaos;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Manutencaos;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class ManutencaoRepository : BaseRepository, IManutencaoRepository
    {
        public void Add(Manutencao manutencao)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(manutencaoID AS INT))+1,1) FROM dbo.Manutencaos");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                ManutencaoDapper manutencaoDapper = manutencao.Map(primaryKey);

                try
                {
                    conn.Insert<ManutencaoDapper>(manutencaoDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Manutencao manutencao)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                ManutencaoDapper manutencaoDapper = manutencao.Map(manutencao.manutencaoID);

                conn.Update<ManutencaoDapper>(manutencaoDapper);
            }
        }

        public Manutencao GetByID(int manutencaoID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Manutencaos WHERE manutencaoID = '{0}'", manutencaoID);

                return conn.Query<Manutencao>(sql).FirstOrDefault();
            }
        }

        public List<Manutencao> GetAll(FilterManutencaoCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT tc.manutencaoID, tc.manutencao, tc.startExecution, tc.endExecution, tc.timeExecution, pv.parameterValue AS statusID, " +
                                           "pv1.parameterValue AS testTypeID, sf.systemFeatureName AS featureID " +
                                           "FROM Manutencaos tc " +
                                           "INNER JOIN ParameterValues pv ON tc.statusID = pv.parameterValueID " +
                                           "INNER JOIN ParameterValues pv1 ON tc.testTypeID = pv1.parameterValueID " +
                                           "INNER JOIN SystemFeatures sf ON tc.featureID = sf.systemFeatureID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.Manutencao))
                    sql += string.Format("AND tc.manutencao LIKE '%{0}%' ", command.Manutencao);

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND tc.statusID = '{0}' ", command.StatusID);

                if (!string.IsNullOrEmpty(command.TestTypeID))
                    sql += string.Format("AND tc.testTypeID = '{0}' ", command.TestTypeID);

                if (!string.IsNullOrEmpty(command.FeatureID))
                    sql += string.Format("AND tc.featureID = '{0}' ", command.FeatureID);

                sql += "ORDER BY manutencaoID";
                return conn.Query<Manutencao>(sql).ToList();
            }
        }

        public void Delete(int manutencaoID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Manutencaos WHERE manutencaoID = '{0}'", manutencaoID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
