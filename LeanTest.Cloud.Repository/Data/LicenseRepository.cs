using Lean.Test.Cloud.Domain.Entities.Licenses;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.Licenses;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class LicenseRepository : BaseRepository, ILicenseRepository
    {
        public void Add(License license)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(licenseID AS INT))+1,1) FROM dbo.Licenses");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                LicenseDapper licenseDapper = license.Map(primaryKey);
                try
                {
                    conn.Insert<LicenseDapper>(licenseDapper);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }

        public void Update(License license)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                LicenseDapper licenseDapper = license.Map(license.licenseID);
                try
                {
                    conn.Update<LicenseDapper>(licenseDapper);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }

        public License GetByID(int licenseID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Licenses WHERE licenseID = '{0}'", licenseID);

                return conn.Query<License>(sql).FirstOrDefault();
            }
        }
        public List<License> GetAll(FilterLicenseCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT l.licenseID, l.licenseCode, c.customerName as customerID, l.hostName, l.license, u.userName as CreatedByID, l.creationDate " +
                    "FROM Licenses l INNER JOIN Customers c on c.customerID = l.customerID INNER JOIN Users u on l.createdByID = u.userID WHERE 1 = 1  ");

                if (!string.IsNullOrEmpty(command.LicenseCode))
                    sql += string.Format("AND l.licenseCode LIKE '%{0}%' ", command.LicenseCode);

                if (!string.IsNullOrEmpty(command.CustomerID))
                    sql += string.Format("AND l.customerID LIKE '%{0}%' ", command.CustomerID);

                if (!string.IsNullOrEmpty(command.HostName))
                    sql += string.Format("AND l.hostName LIKE '%{0}%' ", command.HostName);

                if (!string.IsNullOrEmpty(command.CreatedByID))
                    sql += string.Format("AND l.createdByID LIKE '%{0}%' ", command.CreatedByID);

                sql += "ORDER BY licenseCode";
                return conn.Query<License>(sql).ToList();
            }
        }

        public void Delete(int licenseID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Licenses WHERE licenseID = '{0}'", licenseID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
