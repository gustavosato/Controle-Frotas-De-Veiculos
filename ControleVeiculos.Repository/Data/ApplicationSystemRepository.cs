using Lean.Test.Cloud.Domain.Entities.ApplicationSystems;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.ApplicationSystems;

namespace Lean.Test.Cloud.Repository.Data
{
    public class ApplicationSystemRepository : BaseRepository, IApplicationSystemRepository
    {
        public void Add(ApplicationSystem applicationSystem)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(applicationSystemID AS INT))+1,1) FROM dbo.ApplicationSystems");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                ApplicationSystemDapper applicationSystemDapper = applicationSystem.Map(primaryKey);

                conn.Insert<ApplicationSystemDapper>(applicationSystemDapper);
            }
        }

        public void Update(ApplicationSystem applicationSystem)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                ApplicationSystemDapper applicationSystemDapper = applicationSystem.Map(applicationSystem.applicationSystemID);

                conn.Update<ApplicationSystemDapper>(applicationSystemDapper);
            }
        }

        public ApplicationSystem GetByID(int applicationSystemID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.ApplicationSystems WHERE applicationSystemID = '{0}'", applicationSystemID);

                return conn.Query<ApplicationSystem>(sql).FirstOrDefault();
            }
        }

        public List<ApplicationSystem> GetAll(FilterApplicationSystemCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT a.applicationSystemID, c.customerName as CustomerID, v.parameterValue as applicationTypeID, a.applicationSystemName, " +
                                            "a.description FROM ApplicationSystems a INNER JOIN Customers c ON c.customerID = a.customerID " +
                                            "INNER JOIN ParameterValues v on a.applicationTypeID = v.parameterValueID WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.ApplicationSystemName))
                    sql += string.Format("AND applicationSystemName LIKE '%{0}%' ", command.ApplicationSystemName);

                sql += "ORDER BY applicationSystemName";

                return conn.Query<ApplicationSystem>(sql).ToList();
            }
        }

        public List<ApplicationSystem> GetAll(int customerID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT a.applicationSystemID, a.applicationSystemName " +
                                            "FROM ApplicationSystems a " +
                                            "WHERE a.customerID = {0} ", customerID);

                sql += "ORDER BY a.applicationSystemName";

                return conn.Query<ApplicationSystem>(sql).ToList();
            }
        }
        public string GetApplicationSystemNameByID(int applicationSystemID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT applicationSystemName FROM dbo.ApplicationSystems WHERE applicationSystemID = {0}", applicationSystemID);

                return conn.Query<string>(sql).FirstOrDefault();
            }
        }
        public void Delete(int applicationSystemID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.ApplicationSystems WHERE applicationSystemID = '{0}'", applicationSystemID);

                conn.ExecuteScalar(sql);
            }
        }

    }
}
