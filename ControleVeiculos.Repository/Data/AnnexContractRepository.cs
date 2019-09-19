using Lean.Test.Cloud.Domain.Entities.AnnexContracts;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.AnnexContracts;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class AnnexContractRepository : BaseRepository, IAnnexContractRepository
    {
        public void Add(AnnexContract annexContract)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(annexID AS INT))+1,1) FROM dbo.AnnexContracts");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                AnnexContractDapper annexContractDapper = annexContract.Map(primaryKey);
                try
                {
                    conn.Insert<AnnexContractDapper>(annexContractDapper);
                }
                catch (Exception)
                {

                }
            }
        }

        public void Update(AnnexContract annexContract)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                AnnexContractDapper annexContractDapper = annexContract.Map(annexContract.annexID);

                conn.Update<AnnexContractDapper>(annexContractDapper);
            }
        }

        public AnnexContract GetByID(int annexID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.AnnexContracts WHERE annexID = '{0}'", annexID);

                return conn.Query<AnnexContract>(sql).FirstOrDefault();
            }
        }

        public List<AnnexContract> GetAll(FilterAnnexContractCommand command)
        {   
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT c.contractID as contractID,ac.annexID, pv.parameterValue as extencionPeriodID, ac.startDate, ac.endDate, " +
                                           "ac.summary, p.oportunityCode as oportunytyID " +
                                           "FROM AnnexContracts ac " +
                                           "INNER JOIN ParameterValues pv on ac.extencionPeriodID = pv.parameterValueID " +
                                           "INNER JOIN Contracts c on ac.contractID = c.contractID " +
                                           "INNER JOIN Pipelines p on ac.oportunityID = p.oportunityID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.ExtencionPeriodID))
                    sql += string.Format("AND ac.extencionPeriodID = '{0}' ", command.ExtencionPeriodID);

                if (!string.IsNullOrEmpty(command.ContractID))
                    sql += string.Format("AND ac.contractID = '{0}' ", command.ContractID);

                if (!string.IsNullOrEmpty(command.StartDate))
                    sql += string.Format("AND Convert(date, v.startDate, 103) >= Convert(date, '{0}', 103) ", command.StartDate);

                if (!string.IsNullOrEmpty(command.EndDate))
                    sql += string.Format("AND Convert(date, v.EndDate, 103) <= Convert(date, '{0}', 103) ", command.EndDate);
                
                sql += "ORDER BY annexID";

                return conn.Query<AnnexContract>(sql).ToList();
            }
        }

        public void Delete(int annexID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.AnnexContracts WHERE annexID = '{0}'", annexID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
