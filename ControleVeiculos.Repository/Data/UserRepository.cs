using ControleVeiculos.Domain.Entities.Users;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Users;

namespace ControleVeiculos.Repository.Data
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public string Add(User user)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                    conn.ConnectionString = this.ConnectionString;
                    conn.Open();

                    string sql = string.Format("SELECT ISNULL(MAX(CAST(userID AS INT))+1,1) FROM dbo.Users");
                    int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                    UserDapper userDapper = user.Map(primaryKey);
                try
                {
                    conn.Insert<UserDapper>(userDapper);

                    return primaryKey.ToString();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;

                    return null;
                }
            }
        }

        public void Update(User user)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                UserDapper userDapper = user.Map(user.userID);

                conn.Update<UserDapper>(userDapper);
            }
        }

        public User GetByID(int userID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Users WHERE userID = '{0}'", userID);

                return conn.Query<User>(sql).FirstOrDefault();
            }
        }

        public List<User> GetAll(FilterUserCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("Select userID, userName, email, v.parameterValue as departmentID, v1.parameterValue as functionID, v2.parameterValue as functionLevelID, isAdmin, isActive, " +
                                            "ISNULL(accessToDate, '') as accessToDate, " +
                                            "ISNULL(updateRecordTo, '') as updateRecordTo, " +
                                            "ISNULL(releaseDateUpdateRecordTo, '') as releaseDateUpdateRecordTo, isEmployee " +
                                            "From Users u left Join ParameterValues v on u.departmentID = v.parameterValueID " +
                                            "left join ParameterValues v1 on u.functionID = v1.parameterValueID " +
                                            "left join ParameterValues v2 on u.functionLevelID = v2.parameterValueID Where 1 = 1 ");

                if (!string.IsNullOrEmpty(command.UserName))
                    sql += string.Format("AND userName LIKE '%{0}%' ", command.UserName);

                if (!string.IsNullOrEmpty(command.Email))
                    sql += string.Format("AND email LIKE '%{0}%' ", command.Email);

                if (!string.IsNullOrEmpty(command.DepartmentID))
                    sql += string.Format("AND departmentID = '{0}' ", command.DepartmentID);

                if (!string.IsNullOrEmpty(command.FunctionID))
                    sql += string.Format("AND functionID = '{0}' ", command.FunctionID);

                if (!string.IsNullOrEmpty(command.IsEmplyee))
                    sql += string.Format("AND functionID = '{0}' ", command.IsEmplyee);

                sql += "ORDER BY userName";

                return conn.Query<User>(sql).ToList();
            }
        }

        public List<User> GetAll(int userID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format(@"Select * From Users Where 1 = 1 And isActive = 'True' OR userID = {0} ", userID);
                
                sql += "ORDER BY userName";

                return conn.Query<User>(sql).ToList();
            }
        }

        public List<User> GetAllAssociateUserByCustomerID(FilterUserCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("Select distinct u.userID, userName, isActive From Users u inner join CustomersUsers c on u.userID = c.userID Where c.customerID in (" + command.CustomerID + ") And isActive = 'True' ");

                if (!string.IsNullOrEmpty(command.UserName))
                    sql += string.Format("AND userName LIKE '%{0}%' ", command.UserName);

                sql += "ORDER BY userName";
                return conn.Query<User>(sql).ToList();

            }
        }

        public List<User> GetAllNoAssociateUserByCustomerID(FilterUserCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("Select distinct u.userID, userName, isActive From Users u left join CustomersUsers c on u.userID = c.userID Where isActive = 'True' And u.userID not  in (Select distinct u.userID " +
                                            "From Users u inner join CustomersUsers c on u.userID = c.userID Where c.customerID  in (" + command.CustomerID + "))");

                if (!string.IsNullOrEmpty(command.UserName))
                    sql += string.Format("AND userName LIKE '%{0}%' ", command.UserName);

                sql += "ORDER BY userName";
                return conn.Query<User>(sql).ToList();
            }
        }

        public List<User> GetAllAssociateUserByDemandID(FilterUserCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("Select distinct u.userID, userName, isActive From Users u inner join DemandsUsers d on u.userID = d.userID Where d.demandID in (" + command.DemandID + ") And isActive = 'True' ");

                if (!string.IsNullOrEmpty(command.UserName))
                    sql += string.Format("AND userName LIKE '%{0}%' ", command.UserName);

                sql += "ORDER BY userName";
                return conn.Query<User>(sql).ToList();
            }
        }

        public List<User> GetAllNoAssociateUserByDemandID(FilterUserCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("Select distinct u.userID, userName, isActive From Users u left join DemandsUsers d on u.userID = d.userID Where isActive = 'True' And u.userID not  in (Select distinct u.userID " +
                                            "From Users u inner join DemandsUsers d on u.userID = d.userID Where d.demandID  in (" + command.DemandID + "))");

                if (!string.IsNullOrEmpty(command.UserName))
                    sql += string.Format("AND userName LIKE '%{0}%' ", command.UserName);

                sql += "ORDER BY userName";
                return conn.Query<User>(sql).ToList();
            }
        }



        public List<User> GetAllAssociateUserByGroupID(FilterUserCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT DISTINCT u.userID, userName, isActive " +
                                           "FROM Users u " +
                                           "INNER JOIN GroupsUsers d on u.userID = d.userID " +
                                           "WHERE d.groupID IN (" + command.GroupID + ") AND isActive = 'True' ");

                if (!string.IsNullOrEmpty(command.UserName))
                    sql += string.Format("AND userName LIKE '%{0}%' ", command.UserName);

                sql += "ORDER BY userName";
                return conn.Query<User>(sql).ToList();
            }
        }

        public List<User> GetAllNoAssociateUserByGroupID(FilterUserCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT DISTINCT u.userID, userName, isActive " +
                                           "FROM Users u LEFT JOIN GroupsUsers d on u.userID = d.userID " +
                                           "WHERE isActive = 'True' AND u.userID NOT IN (SELECT DISTINCT u.userID " +
                                           "FROM Users u INNER JOIN GroupsUsers d on u.userID = d.userID " +
                                           "WHERE d.groupID IN (" + command.GroupID + "))");

                if (!string.IsNullOrEmpty(command.UserName))
                    sql += string.Format("AND userName LIKE '%{0}%' ", command.UserName);

                sql += "ORDER BY userName";
                return conn.Query<User>(sql).ToList();
            }
        }


        public string GetUserNameByID(int userID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT userName FROM dbo.Users WHERE userID = {0}", userID);

                return conn.Query<string>(sql).FirstOrDefault();
            }
        }

        public User GetByEmail(string email)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Users WHERE email = '{0}'", email);
                try
                {
                    return conn.Query<User>(sql).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    return null;
                }
            }
        }

        public void Delete(int userID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Users WHERE userID = '{0}'", userID);
                conn.ExecuteScalar(sql);                    
            }
        }

    }
}
