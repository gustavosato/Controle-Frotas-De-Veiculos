using Lean.Test.Cloud.Domain.Entities.Tasks;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.Tasks;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class TaskRepository : BaseRepository, ITaskRepository
    {
        public string Add(Task task)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(taskID AS INT))+1,1) FROM dbo.Tasks");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                TaskDapper taskDapper = task.Map(primaryKey);
                try
                {
                    conn.Insert<TaskDapper>(taskDapper);
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                }

                return primaryKey.ToString();
            }
        }

        public void Update(Task task)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                TaskDapper taskDapper = task.Map(task.taskID);

                conn.Update<TaskDapper>(taskDapper);
            }
        }

        public Task GetByID(int taskID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Tasks WHERE taskID = '{0}'", taskID);

                return conn.Query<Task>(sql).FirstOrDefault();
            }
        }

        public List<Task> GetAll(FilterTaskCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT taskID, summary, pv1.parameterValue as statusID, " +
                                           "targetDate, u.userName as createdByID, u1.userName as assignToID " +
                                           "FROM Tasks t " +
                                           "LEFT JOIN ParameterValues pv1 on t.statusID = pv1.parameterValueID " +
                                           "LEFT JOIN users u on t.createdByID = u.userID " +
                                           "LEFT JOIN users u1 on t.assignToID = u1.userID " +
                                           "WHERE 1 = 1 ");
             
                if (!string.IsNullOrEmpty(command.Summary))
                    sql += string.Format("AND summary = '{0}' ", command.Summary);

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND statusID = '{0}' ", command.StatusID);

                if (!string.IsNullOrEmpty(command.CreatedByID))
                    sql += string.Format("AND createdByID = '{0}' ", command.CreatedByID);

                if (!string.IsNullOrEmpty(command.AssignToID))
                    sql += string.Format("AND assignToID = '{0}' ", command.AssignToID);

                sql += sql += "ORDER BY t.targetDate";

                return conn.Query<Task>(sql).ToList();
            }
        }
        
        public void Delete(int taskID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Tasks WHERE taskID = '{0}'", taskID);
                conn.ExecuteScalar(sql);
            }
        }

        public List<Task> GetAllKanban(FilterTaskCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT taskID, summary, t.statusID as statusID, " +
                                           "targetDate, u.userName as createdByID, u1.userName as assignToID " +
                                           "FROM Tasks t " +
                                           "LEFT JOIN users u on t.createdByID = u.userID " +
                                           "LEFT JOIN users u1 on t.assignToID = u1.userID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.Summary))
                    sql += string.Format("AND summary LIKE '%{0}%' ", command.Summary);

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND statusID = '{0}' ", command.StatusID);

                if (!string.IsNullOrEmpty(command.CreatedByID))
                    sql += string.Format("AND createdByID = '{0}' ", command.CreatedByID);

                if (!string.IsNullOrEmpty(command.AssignToID))
                    sql += string.Format("AND assignToID = '{0}' ", command.AssignToID);

                sql += sql += "ORDER BY t.targetDate";

                return conn.Query<Task>(sql).ToList();
            }
        }
    }
}
