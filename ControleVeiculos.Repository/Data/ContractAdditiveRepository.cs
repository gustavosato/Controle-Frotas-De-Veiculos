using ControleVeiculos.Domain.Entities.ContractAdditives;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.ContractAdditives;

namespace ControleVeiculos.Repository.Data
{
    public class ContractAdditiveRepository : BaseRepository, IContractAdditiveRepository
    {
        public void Add(ContractAdditive contractAdditive)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(additiveID AS INT))+1,1) FROM dbo.ContractAdditives");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                ContractAdditiveDapper contractAdditiveDapper = contractAdditive.Map(primaryKey);

                conn.Insert<ContractAdditiveDapper>(contractAdditiveDapper);
            }
        }

        public void Update(ContractAdditive contractAdditive)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                ContractAdditiveDapper contractAdditiveDapper = contractAdditive.Map(contractAdditive.additiveID);

                conn.Update<ContractAdditiveDapper>(contractAdditiveDapper);
            }
        }

        public ContractAdditive GetByID(int contractAdditiveID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.ContractAdditives WHERE additiveID = '{0}'", contractAdditiveID);

                return conn.Query<ContractAdditive>(sql).FirstOrDefault();
            }
        }

        public List<ContractAdditive> GetAll(FilterContractAdditiveCommand command)
        {   
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT co.contractID as contractID,ca.additiveID,  ca.additiveObject, ca.startDate, ca.endDate, " +
                                           "pv.parameterValue AS periodValidityID, pv1.parameterValue AS extencionID, pv2.parameterValue AS extencionPeriodID," +
                                           "pv3.parameterValue AS resetModalityID, ca.billingCondition " +
                                           "FROM ContractAdditives ca " +
                                           "INNER JOIN ParameterValues pv ON ca.periodValidityID = pv.parameterValueID " +
                                           "INNER JOIN ParameterValues pv1 ON ca.extencionID = pv1.parameterValueID " +
                                           "INNER JOIN ParameterValues pv2 ON ca.extencionPeriodID = pv2.parameterValueID " +
                                           "INNER JOIN Contracts co on ca.contractID = co.contractID " +
                                           "INNER JOIN ParameterValues pv3 ON ca.resetModalityID = pv3.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.PeriodValidityID))
                    sql += string.Format("AND v.PeriodValidityID = '{0}' ", command.PeriodValidityID);

                if (!string.IsNullOrEmpty(command.StartDate))
                    sql += string.Format("AND Convert(date, v.startDate, 103) >= Convert(date, '{0}', 103) ", command.StartDate);

                if (!string.IsNullOrEmpty(command.ContractID))
                    sql += string.Format("AND ca.contractID = '{0}' ", command.ContractID);

                if (!string.IsNullOrEmpty(command.EndDate))
                    sql += string.Format("AND Convert(date, v.EndDate, 103) <= Convert(date, '{0}', 103) ", command.EndDate);

                if (!string.IsNullOrEmpty(command.ExtencionID))
                    sql += string.Format("AND v.extencionID = '{0}' ", command.ExtencionID);

                if (!string.IsNullOrEmpty(command.ExtencionPeriodID))
                    sql += string.Format("AND v.extencionPeriodID = '{0}' ", command.ExtencionPeriodID);

                sql += "ORDER BY ca.additiveID";

                return conn.Query<ContractAdditive>(sql).ToList();
            }
        }

        public void Delete(int contractAdditiveID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.ContractAdditives WHERE additiveID = '{0}'", contractAdditiveID);

                conn.ExecuteScalar(sql);
            }
        }

    }
}
