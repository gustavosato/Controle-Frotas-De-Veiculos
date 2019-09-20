using ControleVeiculos.Domain.Entities.DailyLogComments;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.DailyLogComments;

namespace ControleVeiculos.Repository.Data
{
    public class DailyLogCommentRepository : BaseRepository, IDailyLogCommentRepository
    {
        public void Add(DailyLogComment dailyLogComment)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(DailyLogCommentID AS INT))+1,1) FROM dbo.DailyLogComments");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                DailyLogCommentDapper dailyLogCommentDapper = dailyLogComment.Map(primaryKey);

                conn.Insert<DailyLogCommentDapper>(dailyLogCommentDapper);
            }
        }

        public void Update(DailyLogComment dailyLogComment)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                DailyLogCommentDapper dailyLogCommentDapper = dailyLogComment.Map(dailyLogComment.dailyLogsCommentID);

                conn.Update<DailyLogComment>(dailyLogComment);
            }
        }

        public DailyLogComment GetByID(int dailyLogsCommentID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.DailyLogComments WHERE customerID = '{0}'", dailyLogsCommentID);

                return conn.Query<DailyLogComment>(sql).FirstOrDefault();
            }
        }

        public List<DailyLogComment> GetAll(FilterDailyLogCommentCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT * FROM DailyLogComments WHERE 1 =  1 ");

                if (!string.IsNullOrEmpty(command.Descrition))
                    sql += string.Format("AND Descrition LIKE '%{0}%' ", command.Descrition);

                sql += "ORDER BY descrition";
                return conn.Query<DailyLogComment>(sql).ToList();
            }
        }

        public void Delete(int dailyLogsCommentID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.DailyLogComments WHERE applicationID = '{0}'", dailyLogsCommentID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
