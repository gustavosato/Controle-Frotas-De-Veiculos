using Lean.Test.Cloud.Domain.Entities.Issues;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.Issues;

namespace Lean.Test.Cloud.Repository.Data
{
    public class IssueRepository : BaseRepository, IIssueRepository
    {
        public void Add(Issue Issue)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(issueID AS INT))+1,1) FROM dbo.Issues");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                IssueDapper issueDapper = Issue.Map(primaryKey);

                conn.Insert<IssueDapper>(issueDapper);
            }
        }

        public void Update(Issue issue)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                IssueDapper issueDapper = issue.Map(issue.issueID);

                conn.Update<Issue>(issue);
            }
        }

        public Issue GetByID(int issueID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Issues WHERE issueID = '{0}'", issueID);

                return conn.Query<Issue>(sql).FirstOrDefault();
            }
        }

        public List<Issue> GetAll(FilterIssueCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT * FROM Issue WHERE 1 =  1 ");

                if (!string.IsNullOrEmpty(command.Summary))
                    sql += string.Format("AND issueID 0", command.Summary);

                sql += "ORDER BY issueSummary";
                return conn.Query<Issue>(sql).ToList();
            }
        }

        public void Delete(int issueID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Issues WHERE issueID = '{0}'", issueID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
