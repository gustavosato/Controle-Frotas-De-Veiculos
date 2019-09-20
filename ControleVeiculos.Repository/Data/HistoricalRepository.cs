using ControleVeiculos.Domain.Entities.Historicals;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Historicals;

namespace ControleVeiculos.Repository.Data
{
    public class HistoricalRepository : BaseRepository, IHistoricalRepository
    {
        public void Add(Historical historical)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(historicalID AS INT))+1,1) FROM dbo.Historicals");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                HistoricalDapper historicalDapper = historical.Map(primaryKey);

                conn.Insert<HistoricalDapper>(historicalDapper);
            }
        }

        public void Update(Historical historical)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                HistoricalDapper historicalDapper = historical.Map(historical.historicalID);

                conn.Update<Historical>(historical);
            }
        }

        public Historical GetByID(int historicalID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Applications WHERE applicationsID = '{0}'", historicalID);

                return conn.Query<Historical>(sql).FirstOrDefault();
            }
        }

        public List<Historical> GetAll(FilterHistoricalCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT h.historicalID, h.fieldName, h.oldValue, h.newValue, u.userName as createdByID, h.creationDate " +
                                            "FROM Historicals h " +
                                            "INNER JOIN Users u on h.createdByID = u.userID " +
                                            "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.RecordID))
                    sql += string.Format("AND recordID = '{0}' ", command.RecordID);

                if (!string.IsNullOrEmpty(command.SystemFeatureID))
                    sql += string.Format("AND systemFeatureID = '{0}' ", command.SystemFeatureID);

                sql += "ORDER BY Convert(datetime, h.creationDate, 103) Desc";

                return conn.Query<Historical>(sql).ToList();
            }
        }

        public void Delete(int systemFeatureID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Historicals WHERE systemFeatureID = '{0}'", systemFeatureID);

                conn.ExecuteScalar(sql);
            }
        }

        public void Delete(string systemFeatureID, int recordID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Historicals WHERE systemFeatureID = '{0}' AND recordID = '{1}'", systemFeatureID, recordID);

                conn.ExecuteScalar(sql);
            }
        }
    }
}
