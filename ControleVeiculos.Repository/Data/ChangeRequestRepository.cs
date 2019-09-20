using ControleVeiculos.Domain.Entities.ChangeRequests;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.ChangeRequests;

namespace ControleVeiculos.Repository.Data
{
    public class ChangeRequestRepository : BaseRepository, IChangeRequestRepository
    {
        public void Add(ChangeRequest changeRequest)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(customerID AS INT))+1,1) FROM dbo.Customers");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                ChangeRequestDapper changeRequestDapper = changeRequest.Map(primaryKey);

                conn.Insert<ChangeRequestDapper>(changeRequestDapper);
            }
        }

        public void Update(ChangeRequest changeRequest)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                ChangeRequestDapper changeRequestDapper = changeRequest.Map(changeRequest.changeRequestID);

                conn.Update<ChangeRequestDapper>(changeRequestDapper);
            }
        }

        public ChangeRequest GetByID(int changeRequestID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.ChangeRequests WHERE changeRequestID = '{0}'", changeRequestID);

                return conn.Query<ChangeRequest>(sql).FirstOrDefault();
            }
        }

        public List<ChangeRequest> GetAll(FilterChangeRequestCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT * FROM ChangeRequests WHERE 1 =  1 ");

                if (!string.IsNullOrEmpty(command.Summary))
                    sql += string.Format("AND Summary LIKE '%{0}%' ", command.Summary);

                sql += "ORDER BY changeRequestID";
                return conn.Query<ChangeRequest>(sql).ToList();
            }
        }

        public void Delete(int changeRequestID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.changeRequests WHERE changeRequestID = '{0}'", changeRequestID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
