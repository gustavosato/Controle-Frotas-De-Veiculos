using Lean.Test.Cloud.Domain.Entities.Contacts;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.Contacts;

namespace Lean.Test.Cloud.Repository.Data
{
    public class ContactRepository : BaseRepository, IContactRepository
    {
        public void Add(Contact Contact)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(contactID AS INT))+1,1) FROM dbo.Contacts");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                ContactDapper contactDapper = Contact.Map(primaryKey);

                conn.Insert<ContactDapper>(contactDapper);
            }
        }

        public void Update(Contact contact)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                ContactDapper contactDapper = contact.Map(contact.contactID);

                conn.Update<ContactDapper>(contactDapper);
            }
        }

        public Contact GetByID(int contactID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Contacts WHERE contactID = '{0}'", contactID);

                return conn.Query<Contact>(sql).FirstOrDefault();
            }
        }

        public List<Contact> GetAll(FilterContactCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT contactID, contactName, cellNumber, telNumber, " +
                                           "pv.parameterValue as functionID, email, pv1.customerName as customerID, c.description " +
                                           "FROM Contacts c " +
                                           "INNER JOIN ParameterValues pv on c.functionID = pv.parameterValueID " +
                                           "INNER JOIN Customers pv1 on c.customerID = pv1.customerID " +
                                           "WHERE 1 = 1 ");


                if (!string.IsNullOrEmpty(command.ContactName))
                    sql += string.Format("AND c.contactName LIKE '%{0}%'", command.ContactName);

                if (!string.IsNullOrEmpty(command.Email))
                    sql += string.Format("AND c.email LIKE '%{0}%'", command.Email);

                if (!string.IsNullOrEmpty(command.CustomerID))
                    sql += string.Format("AND c.customerID = '{0}'", command.CustomerID);

                sql += "ORDER BY contactName";

                return conn.Query<Contact>(sql).ToList();
            }
        }

        public List<Contact> GetAll(int contactID, int customerID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = null;

                sql = string.Format("SELECT contactID, contactName, cellNumber, telNumber, " +
                                           "pv.parameterValue as functionID, email, cs.customerName as customerID, c.description " +
                                           "FROM Contacts c " +
                                           "INNER JOIN ParameterValues pv on c.functionID = pv.parameterValueID " +
                                           "INNER JOIN Customers cs on c.customerID = cs.customerID " +
                                           "WHERE 1 = 1 AND cs.customerID = {0} OR c.contactID = {1} ", customerID, contactID);
                sql += "ORDER BY c.contactName";

                return conn.Query<Contact>(sql).ToList();
            }
        }

        public void Delete(int contactID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Contacts WHERE contactID = '{0}'", contactID);
                conn.ExecuteScalar(sql);
            }
        }
        public string GetContactNameByID(int contatctID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT contactName FROM dbo.Contacts WHERE contactID = {0}", contatctID);

                return conn.Query<string>(sql).FirstOrDefault();

            }
        }
    }
}
