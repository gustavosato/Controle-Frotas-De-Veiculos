using Lean.Test.Cloud.Domain.Entities.VacanciesResumes;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using System;
using Lean.Test.Cloud.Domain.Command.Vacancies;
using Lean.Test.Cloud.Domain.Entities.Vacancies;
using Lean.Test.Cloud.Domain.Command.Resumes;
using Lean.Test.Cloud.Domain.Entities.Resumes;

namespace Lean.Test.Cloud.Repository.Data
{
    public class VacancieResumeRepository : BaseRepository, IVacancieResumeRepository
    {
        public void Add(VacancieResume vacancieResume)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                VacancieResumeDapper vacancieResumeDapper = vacancieResume.Map();
                    conn.Insert<VacancieResumeDapper>(vacancieResumeDapper);
            }
        }

        public void Delete(int vacancieID, int resumeID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.VacanciesResumes WHERE vacancieID = '{0}' AND resumeID = '{1}'", vacancieID, resumeID);

                conn.ExecuteScalar(sql);
            }
        }

        public List<Resume> GetAllAssociateVacancieByResumeID(FilterResumeCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT DISTINCT r.resumeID, r.summary " +
                                           "FROM Resumes r " +
                                           "LEFT JOIN VacanciesResumes vr ON r.resumeID = vr.resumeID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.Summary))
                    sql += string.Format("AND r.summary LIKE '%{0}%' ", command.Summary);

                if (!string.IsNullOrEmpty(command.VacancieID))
                    sql += string.Format("AND vr.vacancieID = '{0}' ", command.VacancieID);

                return conn.Query<Resume>(sql).ToList();
            }
        }

        public List<Resume> GetAllNoAssociateVacancieByResumeID(FilterResumeCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT DISTINCT r.resumeID, r.summary " +
                                           "FROM Resumes r " +
                                           "LEFT JOIN VacanciesResumes vr ON r.resumeID = vr.resumeID " +
                                           "WHERE r.resumeID NOT IN (SELECT DISTINCT r.resumeID FROM Resumes r " +
                                           "INNER JOIN VacanciesResumes vr ON r.resumeID = vr.resumeID " +
                                           "WHERE vr.vacancieID = '{0}' ) ", command.VacancieID);

                if (!string.IsNullOrEmpty(command.Summary))
                    sql += string.Format("AND r.summary LIKE '%{0}%' ", command.Summary);

                sql += "ORDER BY r.summary";
                return conn.Query<Resume>(sql).ToList();
            }
        }

        public List<Resume> GetAllAssociateVacancieByResumeID(string vacancieID, string resumeID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT DISTINCT r.resumeID, r.summary " +
                                           "FROM Resumes r " +
                                           "LEFT JOIN VacanciesResumes vr ON r.resumeID = vr.resumeID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(vacancieID))
                    sql += string.Format("AND vr.vacancieID = '{0}' ", vacancieID);

                if (!string.IsNullOrEmpty(resumeID))
                    sql += string.Format("AND vr.resumeID = '{0}' ", resumeID);

                sql += "ORDER BY r.summary";
                return conn.Query<Resume>(sql).ToList();
            }
        }

    }
}
