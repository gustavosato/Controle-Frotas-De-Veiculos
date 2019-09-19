using Lean.Test.Cloud.Domain.Entities.Resumes;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.Resumes;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class ResumeRepository : BaseRepository, IResumeRepository
    {
        public string Add(Resume Resume)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(resumeID AS INT))+1,1) FROM dbo.Resumes");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                ResumeDapper resumeDapper = Resume.Map(primaryKey);
                try
                {
                    conn.Insert<ResumeDapper>(resumeDapper);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return primaryKey.ToString();
            }
        }

        public void Update(Resume resume)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();


                ResumeDapper resumeDapper = resume.Map(resume.resumeID);

                conn.Update<ResumeDapper>(resumeDapper);
            }
        }

        public Resume GetByID(int resumeID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Resumes WHERE resumeID = '{0}'", resumeID);

                return conn.Query<Resume>(sql).FirstOrDefault();
            }
        }

        public List<Resume> GetAll(FilterResumeCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT r.resumeID, r.summary, r.timeExperience, pv.parameterValue as functionID, pv1.parameterValue as functionLevelID, " +
                                            "pv2.parameterValue as statusRhID, pv3.parameterValue as statusManagerID, pv4.parameterValue as statusClientID, " +
                                            "pv5.parameterValue as contractTypeID " +
                                            "FROM Resumes r " +
                                            "INNER JOIN ParameterValues pv ON r.functionID = pv.parameterValueID " +
                                            "INNER JOIN ParameterValues pv1 ON r.functionLevelID = pv1.parameterValueID " +
                                            "INNER JOIN ParameterValues pv2 ON r.statusRhID = pv2.parameterValueID " +
                                            "INNER JOIN ParameterValues pv3 ON r.statusManagerID = pv3.parameterValueID " +
                                            "INNER JOIN ParameterValues pv4 ON r.statusClientID = pv4.parameterValueID " +
                                            "INNER JOIN ParameterValues pv5 ON r.contractTypeID = pv5.parameterValueID " +
                                            "WHERE 1 = 1");                                                                                 

                if (!string.IsNullOrEmpty(command.Summary))
                    sql += string.Format("AND r.summary LIKE '%{0}%' ", command.Summary);

                if (!string.IsNullOrEmpty(command.TimeExperience))
                    sql += string.Format("AND r.timeExperience LIKE '%{0}%' ", command.TimeExperience);

                if (!string.IsNullOrEmpty(command.FunctionID))
                    sql += string.Format("AND r.functionID = '{0}' ", command.FunctionID);

                if (!string.IsNullOrEmpty(command.FunctionLevelID))
                    sql += string.Format("AND r.functionLevelID = '{0}' ", command.FunctionLevelID);

                if (!string.IsNullOrEmpty(command.StatusRhID))
                    sql += string.Format("AND r.statusRhID = '{0}' ", command.StatusRhID);

                if (!string.IsNullOrEmpty(command.StatusManagerID))
                    sql += string.Format("AND r.statusManagerID = '{0}' ", command.StatusManagerID);

                if (!string.IsNullOrEmpty(command.StatusClientID))
                    sql += string.Format("AND r.statusClientID = '{0}' ", command.StatusClientID);

                if (!string.IsNullOrEmpty(command.ContractTypeID))
                    sql += string.Format("AND r.contractTypeID = '{0}' ", command.ContractTypeID);

                sql += "ORDER BY r.summary";

                return conn.Query<Resume>(sql).ToList();
            }
        }

        public void Delete(int resumeID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Resumes WHERE resumeID = '{0}'", resumeID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
