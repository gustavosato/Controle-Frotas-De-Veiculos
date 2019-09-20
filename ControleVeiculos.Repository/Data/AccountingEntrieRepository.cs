using ControleVeiculos.Domain.Entities.AccountingEntries;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.AccountingEntries;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class AccountingEntrieRepository : BaseRepository, IAccountingEntrieRepository
    {
        public string Add(AccountingEntrie accountingEntrie)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(accountingEntrieID AS INT))+1,1) FROM dbo.AccountingEntries");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                AccountingEntrieDapper accountingEntrieDapper = accountingEntrie.Map(primaryKey);

                conn.Insert<AccountingEntrieDapper>(accountingEntrieDapper);

                return primaryKey.ToString();
            }
        }

        public void Update(AccountingEntrie accountingEntrie)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                AccountingEntrieDapper accountingEntrieDapper = accountingEntrie.Map(accountingEntrie.accountingEntrieID);

                conn.Update<AccountingEntrieDapper>(accountingEntrieDapper);
            }
        }

        public AccountingEntrie GetByID(int accountingEntrieID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.AccountingEntries WHERE accountingEntrieID = '{0}'", accountingEntrieID);

                return conn.Query<AccountingEntrie>(sql).FirstOrDefault();
            }
        }

        public List<AccountingEntrie> GetAll(FilterAccountingEntrieCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT c.customerName as customerID, accountingEntrieID, d.demandCode + ' - ' + d.demandName as demandID, v.parameterValue as statusID, " +
                                            "FORMAT(Convert(float, replace(valueToBeRealized, ',', '.')), 'c', 'pt-br') as valueToBeRealized, " +
                                            "b.competitionDate, " +
                                            "FORMAT(Convert(float, replace(realizedValue, ',', '.')), 'c', 'pt-br') as realizedValue, " +
                                            "b.realizedDate, b.invoiceNumber, " +
                                            "u.userName as createdByID " +
                                            "FROM AccountingEntries b INNER JOIN Demands d on b.demandID = d.demandID INNER JOIN ParameterValues v on b.statusID = v.parameterValueID " +
                                            "INNER JOIN Customers c on c.customerID = b.customerID "  +
                                            "INNER JOIN Users u on b.createdByID = u.userID " +

                                            "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.CompetitionStartDate))
                    sql += string.Format("AND Convert(date, competitionDate, 103) >= Convert(date, '{0}', 103) ", command.CompetitionStartDate);

                if (!string.IsNullOrEmpty(command.CompetitionEndDate))
                    sql += string.Format("AND Convert(date, competitionDate, 103) <= Convert(date, '{0}', 103) ", command.CompetitionEndDate);

                if (!string.IsNullOrEmpty(command.DemandID))
                    sql += string.Format("AND b.demandID = '{0}' ", command.DemandID);

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND b.statusID = '{0}' ", command.StatusID);

                if (!string.IsNullOrEmpty(command.InvoiceNumber))
                    sql += string.Format("AND b.invoiceNumber = '{0}' ", command.InvoiceNumber);

                if (!string.IsNullOrEmpty(command.ValueToBeRealized))
                    sql += string.Format("AND b.valueToBeRealized LIKE REPLACE(REPLACE('%{0}%', 'R$ ', ''), '.', '') ", command.ValueToBeRealized);

                if (!string.IsNullOrEmpty(command.RealizedValue))
                    sql += string.Format("AND b.realizedValue LIKE REPLACE(REPLACE('%{0}%', 'R$ ', ''), '.', '') ", command.RealizedValue);

                sql += "ORDER BY Convert(datetime, b.CreationDate, 103) DESC";

                return conn.Query<AccountingEntrie>(sql).ToList();
            }
        }

        public void Delete(int accountingEntrieID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.AccountingEntries WHERE accountingEntrieID = '{0}'", accountingEntrieID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
