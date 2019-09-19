using Lean.Test.Cloud.Domain.Entities.Expenses;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.Expenses;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class ExpenseRepository : BaseRepository, IExpenseRepository
    {
        public string Add(Expense expense)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(expenseID AS INT))+1,1) FROM dbo.Expenses");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                ExpenseDapper expenseDapper = expense.Map(primaryKey);

                conn.Insert<ExpenseDapper>(expenseDapper);

                return primaryKey.ToString();
            }
        }

        public void Update(Expense expense)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                ExpenseDapper expenseDapper = expense.Map(expense.expenseID);
                conn.Update<ExpenseDapper>(expenseDapper);
            }
        }

        public Expense GetByID(int expenseID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Expenses WHERE expenseID = '{0}'", expenseID);

                return conn.Query<Expense>(sql).FirstOrDefault();
            }
        }

        public List<Expense> GetAll(FilterExpenseCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT c.customerName as customerID, v2.parameterValue as departmentID, d.demandCode + ' - ' + d.demandName as demandID, expenseID, FORMAT(Convert(datetime, registerDate, 103), 'ddd dd/MM/yyyy') as registerDate, " +
                                            "FORMAT(Convert(float,replace(replace(AmountExpense, ',', '.'), 'R$', '')), 'c', 'pt-br') as AmountExpense, v.parameterValue as statusID, v1.parameterValue as typeExpenseID, u.userName as createdByID " +
                                            "FROM Expenses e INNER JOIN Customers c on e.customerID = c.customerID LEFT JOIN Demands d on e.demandID = d.demandID " +
                                            "INNER JOIN ParameterValues v on e.statusID = v.parameterValueID INNER JOIN ParameterValues v1 on e.typeExpenseID = v1.parameterValueID INNER JOIN ParameterValues v2 on e.departmentID = v2.parameterValueID " +
                                            "INNER JOIN users u on e.createdByID = u.userID WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.CreatedByID))
                    sql += string.Format("AND e.createdByID = '{0}' ", command.CreatedByID);

                if (!string.IsNullOrEmpty(command.CustomerID))
                    sql += string.Format("AND e.customerID = '{0}' ", command.CustomerID);

                if (!string.IsNullOrEmpty(command.DemandID))
                    sql += string.Format("AND e.demandID = '{0}' ", command.DemandID);

                if (!string.IsNullOrEmpty(command.Description))
                    sql += string.Format("AND e.description LIKE '%{0}%' ", command.Description);

                if (!string.IsNullOrEmpty(command.RegisterDateFrom))
                    sql += string.Format("AND Convert(date, e.registerDate, 103) >= Convert(date, '{0}', 103) ", command.RegisterDateFrom);

                if (!string.IsNullOrEmpty(command.RegisterDateTo))
                    sql += string.Format("AND Convert(date, e.registerDate, 103) <= Convert(date, '{0}', 103) ", command.RegisterDateTo);

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND e.statusID= '{0}' ", command.StatusID);

                if (!string.IsNullOrEmpty(command.TypeExpenseID))
                    sql += string.Format("AND e.typeExpenseID = '{0}' ", command.TypeExpenseID);

                if (!string.IsNullOrEmpty(command.DepartmentID))
                    sql += string.Format("AND e.departmentID = '{0}' ", command.DepartmentID);

                sql += "ORDER BY Convert(datetime, e.creationDate, 103) DESC";
                return conn.Query<Expense>(sql).ToList();
            }
        }

        public void Delete(int expenseID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Expenses WHERE expenseID = '{0}'", expenseID);
                conn.ExecuteScalar(sql);
            }
        }

        public List<Expense> GetTotalByUsers(FilterExpenseCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("select u.userName as createdByID,  pv.parameterValue as statusID,  " +
                                            "'R$' + replace(CONVERT(varchar, sum(cast(replace(e.AmountExpense, ',', '.') as decimal(18,2)))), '.', ',') as AmountExpense " +
                                            "from Expenses e " +
                                            "inner join Users u on e.createdByID = u.userID " +
                                            "inner join ParameterValues pv on e.statusID = pv.parameterValueID " +
                                            "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.CreatedByID))
                    sql += string.Format("AND e.createdByID = '{0}' ", command.CreatedByID);

                if (!string.IsNullOrEmpty(command.CustomerID))
                    sql += string.Format("AND e.customerID = '{0}' ", command.CustomerID);

                if (!string.IsNullOrEmpty(command.DemandID))
                    sql += string.Format("AND e.demandID = '{0}' ", command.DemandID);

               if (!string.IsNullOrEmpty(command.RegisterDateFrom))
                    sql += string.Format("AND Convert(date, e.creationDate, 103) >= Convert(date, '{0}', 103) ", command.RegisterDateFrom);

                if (!string.IsNullOrEmpty(command.RegisterDateTo))
                    sql += string.Format("AND Convert(date, e.creationDate, 103) <= Convert(date, '{0}', 103) ", command.RegisterDateTo);

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND e.statusID= '{0}' ", command.StatusID);

                if (!string.IsNullOrEmpty(command.TypeExpenseID))
                    sql += string.Format("AND e.typeExpenseID = '{0}' ", command.TypeExpenseID);

                if (!string.IsNullOrEmpty(command.DepartmentID))
                    sql += string.Format("AND e.departmentID = '{0}' ", command.DepartmentID);

                sql += "Group By u.userName, pv.parameterValue Order By 1";

                return conn.Query<Expense>(sql).ToList();
            }
        }

    }
}
