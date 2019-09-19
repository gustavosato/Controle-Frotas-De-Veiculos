using Lean.Test.Cloud.Domain.Entities.Customers;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.Customers;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public void Add(Customer customer)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(customerID AS INT))+1,1) FROM dbo.Customers");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                CustomerDapper customerDapper = customer.Map(primaryKey);
                try
                {
                    conn.Insert<CustomerDapper>(customerDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Customer customer)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                CustomerDapper customerDapper = customer.Map(customer.customerID);
                try
                {
                    conn.Update<CustomerDapper>(customerDapper);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }

        public Customer GetByID(int customerID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Customers WHERE customerID = '{0}'", customerID);

                return conn.Query<Customer>(sql).FirstOrDefault();
            }
        }

        public List<Customer> GetAll(FilterCustomerCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT c.customerID, c.customerName, pv1.parameterValue as segmentID, pv2.parameterValue as typeID, c.isActive " +
                                           "FROM Customers c " +
                                           "LEFT JOIN ParameterValues pv1 on c.segmentID = pv1.parameterValueID " +
                                           "LEFT JOIN ParameterValues pv2 on c.typeID = pv2.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.CustomerName))
                    sql += string.Format("AND customerName LIKE '%{0}%' ", command.CustomerName);

                if (!string.IsNullOrEmpty(command.SegmentID))
                    sql += string.Format("AND segmentID = '{0}' ", command.SegmentID);

                if (!string.IsNullOrEmpty(command.TypeID))
                    sql += string.Format("AND typeID IN ('{0}') ", command.TypeID);

                if (command.IsActive)
                    sql += string.Format("AND isActive = '{0}' ", command.IsActive);

                if (!string.IsNullOrEmpty(command.CustomerID))
                    sql += string.Format("OR customerID = '{0}' ", command.CustomerID);

                sql += "ORDER BY customerName";

                return conn.Query<Customer>(sql).ToList();
            }
        }

        public void Delete(int customerID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Customers WHERE customerID = '{0}'", customerID);
                conn.ExecuteScalar(sql);
            }
        }

        public List<Customer> GetAllAssociateCustomerByUserID(FilterCustomerCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT DISTINCT c.customerID, c.customerName, c.isActive, cs.userID FROM Customers c " +
                                            "LEFT JOIN CustomersUsers cs ON c.customerID = cs.customerID WHERE cs.userID IN('{0}') AND c.isActive = 'True' ", command.UserID);

                if (!string.IsNullOrEmpty(command.CustomerName))
                    sql += string.Format("AND c.customerName LIKE '%{0}%' ", command.CustomerName);

                sql += "ORDER BY c.customerName";
                return conn.Query<Customer>(sql).ToList();
            }
        }

        public List<Customer> GetAllAssociateCustomerByUserID(string userID, string customerID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT DISTINCT c.customerID, c.customerName, c.isActive FROM Customers c " +
                                            "Inner JOIN CustomersUsers cs ON c.customerID = cs.customerID " +
                                            "WHERE cs.userID IN('{0}') AND c.isActive = 'True' ", userID, customerID);

                sql += "ORDER BY c.customerName";
                return conn.Query<Customer>(sql).ToList();
            }
        }

        public List<Customer> GetAllNoAssociateCustomerByUserID(FilterCustomerCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("Select distinct c.customerID, c.customerName, c.isActive " +
                                           "From Customers c left join CustomersUsers uc on c.customerID = uc.customerID " +
                                           "Where c.customerID not in (Select distinct c.customerID From Customers c inner join CustomersUsers uc on c.customerID = uc.customerID " +
                                           "Where uc.userID in ({0})) And c.isActive = 'True' ", command.UserID);

                if (!string.IsNullOrEmpty(command.CustomerName))
                    sql += string.Format("AND c.customerName LIKE '%{0}%' ", command.CustomerName);

                sql += "ORDER BY c.customerName";
                return conn.Query<Customer>(sql).ToList();
            }
        }

        public string GetCustomerNameByID(int customerID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT customerName FROM dbo.Customers WHERE customerID = {0}", customerID);

                return conn.Query<string>(sql).FirstOrDefault();
            }
        }

        public List<Customer> GetAllGroupCompanies(string customerID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT DISTINCT c.customerID, c.customerName " +
                                            "FROM Customers c " +
                                            "WHERE c.isActive = 'True' AND c.typeID = '300302304' OR c.customerID = '{0}'", customerID);

                sql += "ORDER BY c.customerName";

                return conn.Query<Customer>(sql).ToList();
            }
        }

        public List<Customer> GetAllNoGroupCompanies(string customerID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT DISTINCT c.customerID, c.customerName " +
                                           "FROM Customers c " +
                                           "WHERE c.isActive = 'True' AND c.typeID <> '300302304' OR c.customerID = '{0}'", customerID);

                sql += "ORDER BY c.customerName";

                return conn.Query<Customer>(sql).ToList();
            }
        }
    }
}

