using ControleVeiculos.Domain.Entities.Supports;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Supports;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class SupportRepository : BaseRepository, ISupportRepository
    {
        public string Add(Support Support)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(supportID AS INT))+1,1) FROM dbo.Supports");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                SupportDapper supportDapper = Support.Map(primaryKey);

                try
                {
                    conn.Insert<SupportDapper>(supportDapper);

                    return primaryKey.ToString();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public void Update(Support support)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();


                SupportDapper supportDapper = support.Map(support.supportID);

                conn.Update<SupportDapper>(supportDapper);
            }
        }

        public Support GetByID(int supportID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Supports WHERE supportID = '{0}'", supportID);

                return conn.Query<Support>(sql).FirstOrDefault();
            }
        }

        public List<Support> GetAll(FilterSupportCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT supportID,pv.parameterValue as statusID,summary, " +
                                          "pv1.parameterValue as severityID,pv2.parameterValue as typeID, " +
                                          " pv3.parameterValue as priorityID, u1.customerName as customerID, u.userName as assingToID " +
                                          "FROM Supports s " +
                                          "INNER JOIN ParameterValues pv on s.statusID = pv.parameterValueID " +
                                          "INNER JOIN ParameterValues pv1 on s.severityID = pv1.parameterValueID " +
                                          "INNER JOIN ParameterValues pv2 on s.typeID = pv2.parameterValueID " +
                                          "INNER JOIN ParameterValues pv3 on s.priorityID = pv3.parameterValueID " +
                                          "INNER JOIN Customers u1 on s.customerID = u1.customerID " +
                                          "INNER JOIN Users u on s.assingToID = u.userID " +
                                          "WHERE 1 = 1 ");


                if (!string.IsNullOrEmpty(command.Summary))
                    sql += string.Format("AND s.summary LIKE '%{0}%'", command.Summary);

                if (!string.IsNullOrEmpty(command.TypeID))
                    sql += string.Format("AND s.typeID = '{0}'", command.TypeID);

                if (!string.IsNullOrEmpty(command.SeverityID))
                    sql += string.Format("AND s.severityID = '{0}'", command.SeverityID);

                if (!string.IsNullOrEmpty(command.PriorityID))
                    sql += string.Format("AND s.priorityID = '{0}'", command.PriorityID);

                if (!string.IsNullOrEmpty(command.AssingToID))
                    sql += string.Format("AND s.assingToID = '{0}'", command.AssingToID);

                if (!string.IsNullOrEmpty(command.CustomerID))
                    sql += string.Format("AND s.customerID = '{0}'", command.CustomerID);

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND s.statusID = '{0}'", command.StatusID);

                if (!string.IsNullOrEmpty(command.CreatedByID))
                    sql += string.Format("AND s.createdByID = '{0}'", command.CreatedByID);

                sql += "ORDER BY supportID";

                return conn.Query<Support>(sql).ToList();
            }
        }

        public List<Support> GetAll(int supportID, int customerID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = null;

                sql = string.Format("SELECT supportID,pv.parameterValue as statusID,summary, " +
                                    "pv1.parameterValue as severityID,pv2.parameterValue as typeID, " +
                                    " pv3.parameterValue as priorityID, u1.customerName as customerID, u.userName as assingToID " +
                                    "FROM Supports s " +
                                    "INNER JOIN ParameterValues pv on s.statusID = pv.parameterValueID " +
                                    "INNER JOIN ParameterValues pv1 on s.severityID = pv1.parameterValueID " +
                                    "INNER JOIN ParameterValues pv2 on s.typeID = pv2.parameterValueID " +
                                    "INNER JOIN ParameterValues pv3 on s.priorityID = pv3.parameterValueID " +
                                    "INNER JOIN Customers u1 on s.customerID = u1.customerID " +
                                    "INNER JOIN Users u on s.assingToID = u.userID " +
                                    "WHERE 1 = 1 ");

                sql += "ORDER BY s.summary";

                return conn.Query<Support>(sql).ToList();
            }
        }
        public void Delete(int supportID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Supports WHERE supportID = '{0}'", supportID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
