using ControleVeiculos.Domain.Entities.Contracts;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Contracts;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class ContractRepository : BaseRepository, IContractRepository
    {
        public void Add(Contract contract)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(contractID AS INT))+1,1) FROM dbo.Contracts");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                ContractDapper contractDapper = contract.Map(primaryKey);

                conn.Insert<ContractDapper>(contractDapper);
            }
        }

        public void Update(Contract contract)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                ContractDapper contractDapper = contract.Map(contract.contractID);

                conn.Update<ContractDapper>(contractDapper);
            }
        }

        public Contract GetByID(int contractID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Contracts WHERE contractID = '{0}'", contractID);

                return conn.Query<Contract>(sql).FirstOrDefault();
            }
        }

        public List<Contract> GetAll(FilterContractCommand command)
        {   
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT c.contractID, pv.parameterValue as contractTypeID, pv1.parameterValue as periodValidityID, " +
                                           "pv2.parameterValue as extencionID, pv3.parameterValue as extencionPeriodID, pv4.parameterValue as resetModality, " +
                                           "u.userName as createdByID, c.startDate, c.endDate, ct.customerName as contractorCustomerID, ct1.customerName as contractingCustomerID, " +
                                           "p.oportunityCode as oportunityID " +
                                           "FROM Contracts c " +
                                           "INNER JOIN ParameterValues pv on c.contractTypeID = pv.parameterValueID " +
                                           "INNER JOIN ParameterValues pv1 on c.periodValidityID = pv1.parameterValueID " +
                                           "INNER JOIN ParameterValues pv2 on c.extencionID = pv2.parameterValueID " +
                                           "INNER JOIN ParameterValues pv3 on c.extencionPeriodID = pv3.parameterValueID " +
                                           "INNER JOIN ParameterValues pv4 on c.resetModalityID = pv4.parameterValueID " +
                                           "INNER JOIN Customers ct on c.contractorCustomerID = ct.customerID " +
                                           "INNER JOIN Customers ct1 on c.contractingCustomerID = ct1.customerID " +
                                           "INNER JOIN Users u on c.createdByID = u.userID " +
                                           "INNER JOIN Pipelines p on c.oportunityID = p.oportunityID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.OportunityID))
                    sql += string.Format("AND c.oportunityID = '{0}' ", command.OportunityID);

                if (!string.IsNullOrEmpty(command.ContractTypeID))
                    sql += string.Format("AND c.contractTypeID = '{0}' ", command.ContractTypeID);

                if (!string.IsNullOrEmpty(command.ContractorCustomerID))
                    sql += string.Format("AND c.contractorCustomerID = '{0}' ", command.ContractorCustomerID);

                if (!string.IsNullOrEmpty(command.ContractingCustomerID))
                    sql += string.Format("AND c.contractingCustomerID = '{0}' ", command.ContractingCustomerID);

                if (!string.IsNullOrEmpty(command.StartDate))
                    sql += string.Format("AND Convert(date, c.startDate, 103) >= Convert(date, '{0}', 103) ", command.StartDate);

                if (!string.IsNullOrEmpty(command.EndDate))
                    sql += string.Format("AND Convert(date, c.endDate, 103) <= Convert(date, '{0}', 103) ", command.EndDate);

                if (!string.IsNullOrEmpty(command.PeriodValidityID))
                    sql += string.Format("AND c.periodValidityID = '{0}' ", command.PeriodValidityID);

                if (!string.IsNullOrEmpty(command.ExtencionID))
                    sql += string.Format("AND c.extencionID = '{0}' ", command.ExtencionID);

                if (!string.IsNullOrEmpty(command.ExtencionPeriodID))
                    sql += string.Format("AND c.extencionPeriodID = '{0}' ", command.ExtencionPeriodID);

                if (!string.IsNullOrEmpty(command.ResetModalityID))
                    sql += string.Format("AND c.resetModalityID = '{0}' ", command.ResetModalityID);

                sql += "ORDER BY contractID, Convert(datetime, c.creationDate, 103)";

                return conn.Query<Contract>(sql).ToList();
            }
        }

        public List<Contract> GetAll(int contractID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT c.contractID, pv.parameterValue as contractTypeID, pv1.parameterValue as periodValidityID, " +
                                           "pv2.parameterValue as extencionID, pv3.parameterValue as extencionPeriodID, pv4.parameterValue as resetModality, " +
                                           "u.userName as createdByID, c.startDate, c.endDate, ct.customerName as contractorCustomerID, ct1.customerName as contractingCustomerID, " +
                                           "p.oportunityCode as oportunityID " +
                                           "FROM Contracts c " +
                                           "INNER JOIN ParameterValues pv on c.contractTypeID = pv.parameterValueID " +
                                           "INNER JOIN ParameterValues pv1 on c.periodValidityID = pv1.parameterValueID " +
                                           "INNER JOIN ParameterValues pv2 on c.extencionID = pv2.parameterValueID " +
                                           "INNER JOIN ParameterValues pv3 on c.extencionPeriodID = pv3.parameterValueID " +
                                           "INNER JOIN ParameterValues pv4 on c.resetModalityID = pv4.parameterValueID " +
                                           "INNER JOIN Customers ct on c.contractorCustomerID = ct.customerID " +
                                           "INNER JOIN Customers ct1 on c.contractingCustomerID = ct1.customerID " +
                                           "INNER JOIN Users u on c.createdByID = u.userID " +
                                           "INNER JOIN Pipelines p on c.oportunityID = p.oportunityID " +
                                           "WHERE 1 = 1 ");


                sql += "ORDER BY p.oportunityCode DESC";

                return conn.Query<Contract>(sql).ToList();
            }
        }

        public void Delete(int contractID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Contracts WHERE contractID = '{0}'", contractID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
