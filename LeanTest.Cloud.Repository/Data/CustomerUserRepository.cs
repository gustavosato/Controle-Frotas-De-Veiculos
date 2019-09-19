using Lean.Test.Cloud.Domain.Entities.CustomersUsers;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.CustomersUsers;
using System;
using Lean.Test.Cloud.Domain.Command.Customers;
using Lean.Test.Cloud.Domain.Entities.Customers;


namespace Lean.Test.Cloud.Repository.Data
{
    public class CustomerUserRepository : BaseRepository, ICustomerUserRepository
    {
        public void Add(CustomerUser customerUser)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                CustomerUserDapper customerUserDapper = customerUser.Map();
                conn.Insert<CustomerUserDapper>(customerUserDapper);
            }
        }

        public void Delete(int customerID, int userID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.CustomersUsers WHERE customerID = '{0}' AND userID = '{1}'", customerID, userID);
                conn.ExecuteScalar(sql);
                }
        }

        public List<Customer> GetAllAssociateCustomerByUserID(FilterCustomerCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT DISTINCT c.customerID, c.customerName " +
                                           "FROM Customers c " +
                                           "LEFT JOIN CustomersUsers cs ON c.customerID = cs.customerID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.CustomerName))
                    sql += string.Format("AND g.customerName LIKE '%{0}%' ", command.CustomerName);

                if (!string.IsNullOrEmpty(command.UserID))
                    sql += string.Format("AND cs.userID = '{0}' ", command.UserID);

                return conn.Query<Customer>(sql).ToList();
            }
        }

        public List<Customer> GetAllNoAssociateCustomerByUserID(FilterCustomerCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT DISTINCT c.customerID, c.customerName " +
                                           "FROM Customers c " +
                                           "LEFT JOIN CustomersUsers cs ON c.customerID = cs.customerID " +
                                           "WHERE c.customerID NOT IN (SELECT DISTINCT c.customerID FROM Customers c " +
                                           "INNER JOIN CustomersUsers cs ON c.customerID = cs.customerID " +
                                           "WHERE cs.userID = '{0}' ) ", command.UserID);

                if (!string.IsNullOrEmpty(command.CustomerName))
                    sql += string.Format("AND c.customerName LIKE '%{0}%' ", command.CustomerName);

                sql += "ORDER BY c.customerName";
                return conn.Query<Customer>(sql).ToList();
            }
        }

    }
}
