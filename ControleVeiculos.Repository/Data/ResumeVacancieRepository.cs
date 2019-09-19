using Lean.Test.Cloud.Domain.Entities.ResumeVacancies;
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
    public class ResumeVacancieRepository : BaseRepository, IResumeVacancieRepository
    {
        public void Add(ResumeVacancie resumeVacancie)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                ResumeVacancieDapper resumeVacancieDapper = resumeVacancie.Map();
                    conn.Insert<ResumeVacancieDapper>(resumeVacancieDapper);
            }
        }

        public void Delete(int resumeID, int vacancieID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.ResumeVacancies WHERE resumeID = '{0}' AND vacancieID = '{1}'", resumeID, vacancieID);

                conn.ExecuteScalar(sql);
            }
        }

        public List<Vacancie> GetAllAssociateResumeByVacancieID(FilterVacancieCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
               
                string sql = string.Format("SELECT * FROM Vacancies " +
                                           "WHERE resumeSelectedID = '{0}' ", command.ResumeID);
                
                sql += "ORDER BY resumeSelectedID";
                return conn.Query<Vacancie>(sql).ToList();
            }
        }

        public List<Vacancie> GetAllNoAssociateResumeByVacancieID(FilterVacancieCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                
                string sql = string.Format("SELECT DISTINCT v.vacancieID, v.summary " +
                                           "FROM Vacancies v " +
                                           "LEFT JOIN VacanciesResumes vr ON v.vacancieID = vr.vacancieID " +
                                           "WHERE v.vacancieID IN (SELECT DISTINCT v.vacancieID FROM Vacancies v " +
                                           "INNER JOIN VacanciesResumes vr ON v.vacancieID = vr.vacancieID " +
                                           "WHERE vr.resumeID = '{0}')", command.ResumeID);
                
                sql += "ORDER BY v.summary";
                return conn.Query<Vacancie>(sql).ToList();
            }
        }
    }
}
