using Lean.Test.Cloud.Domain.Entities.Workflows;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.Workflows;

namespace Lean.Test.Cloud.Repository.Data
{
    public class WorkflowRepository : BaseRepository, IWorkflowRepository
    {
        public void Add(Workflow workflow)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(workflowID AS INT))+1,1) FROM dbo.Workflows");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                WorkflowDapper workflowDapper = workflow.Map(primaryKey);

                conn.Insert<WorkflowDapper>(workflowDapper);
            }
        }

        public void Update(Workflow workflow)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                WorkflowDapper workflowDapper = workflow.Map(workflow.workflowID);

                conn.Update<Workflow>(workflow);
            }
        }

        public Workflow GetByID(int workflowID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Workflows WHERE workflowID = '{0}'", workflowID);

                return conn.Query<Workflow>(sql).FirstOrDefault();
            }
        }

        public List<Workflow> GetAll(FilterWorkflowCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT * FROM Workflows WHERE 1 =  1 ");

                if (!string.IsNullOrEmpty(command.SystemFeatureID))
                    sql += string.Format("AND systemFeatureID = 0", command.SystemFeatureID);

                sql += "ORDER BY workflowID";
                return conn.Query<Workflow>(sql).ToList();
            }
        }

        public void Delete(int workflowID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Workflows WHERE workflowID = '{0}'", workflowID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
