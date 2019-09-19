using Lean.Test.Cloud.Domain.Entities.Vacancies;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.Vacancies;

namespace Lean.Test.Cloud.Repository.Data
{
    public class VacancieRepository : BaseRepository, IVacancieRepository
    {
        public void Add(Vacancie Vacancie)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(vacancieID AS INT))+1,1) FROM dbo.Vacancies");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                VacancieDapper vacancieDapper = Vacancie.Map(primaryKey);

                conn.Insert<VacancieDapper>(vacancieDapper);
            }
        }

        public void Update(Vacancie vacancie)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                VacancieDapper vacancieDapper = vacancie.Map(vacancie.vacancieID);

                conn.Update<VacancieDapper>(vacancieDapper);
            }
        }

        public Vacancie GetByID(int vacancieID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Vacancies WHERE vacancieID = '{0}'", vacancieID);

                return conn.Query<Vacancie>(sql).FirstOrDefault();
            }
        }

        public List<Vacancie> GetAll(FilterVacancieCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT v.vacancieID, v.summary, pv.parameterValue as vacanciesTypeID, pv1.parameterValue as contractTypeID, c.customerName as customerID, " +
                                           "pv2.parameterValue as validityID, pv3.parameterValue as statusID, v.workPlace, u.userName as createdByID " +
                                           "FROM Vacancies v " +
                                           "INNER JOIN ParameterValues pv on v.vacanciesTypeID = pv.parameterValueID " +
                                           "INNER JOIN ParameterValues pv1 on v.contractTypeID = pv1.parameterValueID " +
                                           "INNER JOIN ParameterValues pv2 on v.validityID = pv2.parameterValueID " +
                                           "INNER JOIN ParameterValues pv3 on v.statusID = pv3.parameterValueID " +
                                           "INNER JOIN Customers c on v.customerID = c.customerID " +
                                           "INNER JOIN Users u on v.createdByID = u.userID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.Summary))
                    sql += string.Format("AND v.summary LIKE '%{0}%'", command.Summary);

                if (!string.IsNullOrEmpty(command.VacanciesTypeID))
                    sql += string.Format("AND v.vacanciesTypeID = '{0}'", command.VacanciesTypeID);

                if (!string.IsNullOrEmpty(command.CustomerID))
                    sql += string.Format("AND v.customerID = '{0}'", command.CustomerID);

                if (!string.IsNullOrEmpty(command.ContractTypeID))
                    sql += string.Format("AND v.contractTypeID = '{0}'", command.ContractTypeID);

                if (!string.IsNullOrEmpty(command.ValidityID))
                    sql += string.Format("AND v.validityID = '{0}'", command.ValidityID);

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND v.statusID = '{0}'", command.StatusID);

                if (!string.IsNullOrEmpty(command.WorkPlace))
                    sql += string.Format("AND v.workPlace LIKE '%{0}%'", command.WorkPlace);

                if (!string.IsNullOrEmpty(command.CreatedByID))
                    sql += string.Format("AND v.createdByID = '{0}'", command.CreatedByID);

                sql += "ORDER BY v.vacancieID";

                return conn.Query<Vacancie>(sql).ToList();
            }
        }

        
        public List<Vacancie> GetAll(int vacancieID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT vacancieID, summary, pv.parameterValue as vacanciesTypeID, u.userName as createdByID " +
                                           "FROM Vacancies v " +
                                           "INNER JOIN ParameterValues pv on v.vacanciesTypeID = pv.parameterValueID " +
                                           "INNER JOIN Users u on v.createdByID = u.userID ");

                sql += "ORDER BY c.vacancieName";

                return conn.Query<Vacancie>(sql).ToList();
            }
        }

        public void Delete(int vacancieID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Vacancies WHERE vacancieID = '{0}'", vacancieID);
                conn.ExecuteScalar(sql);
            }
        }
        public string GetVacancieNameByID(int contatctID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT vacancieName FROM dbo.Vacancies WHERE vacancieID = {0}", contatctID);

                return conn.Query<string>(sql).FirstOrDefault();

            }
        }
    }
}
