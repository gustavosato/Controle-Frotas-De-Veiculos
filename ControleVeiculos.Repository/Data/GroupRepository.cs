using ControleVeiculos.Domain.Entities.Status;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Status;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class StatusRepository : BaseRepository, IStatusRepository
    {
        public void Add(Status Status)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(statusID AS INT))+1,1) FROM dbo.Statuss");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                StatusDapper statusDapper = Status.Map(primaryKey);
                try
                {
                    conn.Insert<StatusDapper>(statusDapper);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Status status)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                StatusDapper statusDapper = status.Map(status.statusID);

                conn.Update<StatusDapper>(statusDapper);
            }
        }

        public Status GetByID(int statusID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Statuss WHERE statusID = '{0}'", statusID);

                return conn.Query<Status>(sql).FirstOrDefault();
            }
        }

        public List<Status> GetAll(FilterStatusCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();


                string sql = string.Format("SELECT statusID, statusName, g.description " +
                                           "FROM Statuss g " +
                                           "WHERE 1 = 1 ");


                //if (!string.IsNullOrEmpty(command.StatusName))
                //    sql += string.Format("AND g.statusName LIKE '%{0}%'", command.StatusName);

                sql += "ORDER BY statusName";

                return conn.Query<Status>(sql).ToList();
            }
        }

        public List<Status> GetAll(int statusID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Statuss  ");

                sql += "ORDER BY statusName";

                return conn.Query<Status>(sql).ToList();
            }
        }

        public void Delete(int statusID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Statuss WHERE statusID = '{0}'", statusID);
                conn.ExecuteScalar(sql);
            }
        }

       public string GetStatusNameByID(int statusID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT statusName FROM dbo.Statuss WHERE statusID = {0}", statusID);

                return conn.Query<string>(sql).FirstOrDefault();

            }
        }
    }
}
