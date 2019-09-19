using Lean.Test.Cloud.Domain.Entities.MovimentEmployees;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.MovimentEmployees;

namespace Lean.Test.Cloud.Repository.Data
{
    public class MovimentEmployeeRepository : BaseRepository, IMovimentEmployeeRepository
    {
        public void Add(MovimentEmployee movimentEmployee)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(movimentEmployeeID AS INT))+1,1) FROM dbo.MovimentEmployees");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                MovimentEmployeeDapper movimentEmployeeDapper = movimentEmployee.Map(primaryKey);

                conn.Insert<MovimentEmployeeDapper>(movimentEmployeeDapper);
            }
        }

        public void Update(MovimentEmployee movimentEmployee)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                MovimentEmployeeDapper movimentEmployeeDapper = movimentEmployee.Map(movimentEmployee.movimentEmployeeID);

                conn.Update<MovimentEmployeeDapper>(movimentEmployeeDapper);
            }
        }

        public MovimentEmployee GetByID(int movimentEmployeeID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.MovimentEmployees WHERE movimentEmployeeID = '{0}'", movimentEmployeeID);

                return conn.Query<MovimentEmployee>(sql).FirstOrDefault();
            }
        }

        public List<MovimentEmployee> GetAll(FilterMovimentEmployeeCommand command)
        {   
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT v.movimentEmployeeID, u.userName as employeeID, pv.parameterValue as movimentEmployeeTypeID, v.startDate, v.endDate, pv1.parameterValue as statusID " +
                                           "FROM MovimentEmployees v " +
                                           "INNER JOIN ParameterValues pv on v.movimentEmployeeTypeID = pv.parameterValueID " +
                                           "INNER JOIN ParameterValues pv1 on v.statusID = pv1.parameterValueID " +
                                           "INNER JOIN Users u on v.employeeID = u.userID Where 1 = 1 ");

                if (!string.IsNullOrEmpty(command.EmployeeID))
                    sql += string.Format("AND v.employeeID = '{0}' ", command.EmployeeID);

                if (!string.IsNullOrEmpty(command.StartDate))
                    sql += string.Format("AND Convert(date, v.startDate, 103) >= Convert(date, '{0}', 103) ", command.StartDate);

                if (!string.IsNullOrEmpty(command.EndDate))
                    sql += string.Format("AND Convert(date, v.EndDate, 103) <= Convert(date, '{0}', 103) ", command.EndDate);

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND v.statusID= '{0}' ", command.StatusID);

                if (!string.IsNullOrEmpty(command.MovimentEmployeeTypeID))
                    sql += string.Format("AND v.movimentEmployeeTypeID = '{0}' ", command.MovimentEmployeeTypeID);

                sql += "ORDER BY movimentEmployeeTypeID, Convert(datetime, v.creationDate, 103)";

                return conn.Query<MovimentEmployee>(sql).ToList();
            }
        }

        public string GetApropriateByRangeTime(int movimentEmployeeID, int employeeID, string startDate, string endDate)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT movimentEmployeeID FROM MovimentEmployees " +
                                           "WHERE employeeID = {0} AND Convert(datetime, '{2}', 103) > Convert(datetime, startDate, 103) AND Convert(datetime, endDate, 103) > Convert(datetime, '{1}', 103) " +
                                           "AND movimentEmployeeID <> {3}", employeeID, startDate, endDate, movimentEmployeeID);


                return conn.Query<string>(sql).FirstOrDefault();
            }
        }


        public void Delete(int movimentEmployeeID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.MovimentEmployees WHERE movimentEmployeeID = '{0}'", movimentEmployeeID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
