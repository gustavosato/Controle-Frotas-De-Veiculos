using ControleVeiculos.Domain.Entities.Pipelines;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Pipelines;

namespace ControleVeiculos.Repository.Data
{
    public class PipelineRepository : BaseRepository, IPipelineRepository
    {
        public string Add(Pipeline pipeline)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(oportunityID AS INT))+1,1) FROM dbo.Pipelines");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                string tempCode = Convert.ToString(primaryKey);

                tempCode = new string('0', 5 - tempCode.Length) + primaryKey;

                switch (pipeline.costCenterID)
                {
                    case "100103107":
                        pipeline.oportunityCode = "RPLT_" + Convert.ToDateTime(DateTime.Today).ToString("yy.MM." + tempCode);
                        break;
                    case "100103103":
                        pipeline.oportunityCode = "RPCS_" + Convert.ToDateTime(DateTime.Today).ToString("yy.MM." + tempCode);
                        break;
                    case "100103105":
                        pipeline.oportunityCode = "RPIN_" + Convert.ToDateTime(DateTime.Today).ToString("yy.MM." + tempCode);
                        break;
                    default:
                        pipeline.oportunityCode = "RPCS_" + Convert.ToDateTime(DateTime.Today).ToString("yy.MM." + tempCode);
                        break;
                }

                PipelineDapper pipelineDapper = pipeline.Map(primaryKey);

                conn.Insert<PipelineDapper>(pipelineDapper);

                return primaryKey.ToString();
            }
        }

        public void Update(Pipeline pipeline)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();


                PipelineDapper pipelineDapper = pipeline.Map(pipeline.oportunityID);

                conn.Update<PipelineDapper>(pipelineDapper);
            }
        }

        public Pipeline GetByID(int oportunityID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Pipelines WHERE oportunityID = '{0}'", oportunityID);

                return conn.Query<Pipeline>(sql).FirstOrDefault();
            }
        }

        public List<Pipeline> GetAll(FilterPipelineCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT oportunityID, p.oportunityCode, p.summary, c.customerName as customerID, pv1.parameterValue as faseID, pv2.parameterValue as priorityID, pv3.parameterValue as typeID, " +
                                            "pv4.parameterValue as costCenterID, pv5.parameterValue as offerID, pv6.parameterValue as statusID, " +
                                            "FORMAT(Convert(float,replace(replace(p.expectedValue, ',', '.'), 'R$', '')), 'c', 'pt-br') as expectedValue," +
                                            "FORMAT(Convert(float,replace(replace(p.billed, ',', '.'), 'R$', '')), 'c', 'pt-br') as billed," +
                                            "u1.userName as saleManagerID, u2.userName as ownerID, u3.userName as preSalesID, u4.userName as operationManagerID " +
                                            "FROM Pipelines p " +
                                            "INNER JOIN Customers c on p.customerID = c.customerID " +
                                            "INNER JOIN ParameterValues pv1 on p.faseID = pv1.parameterValueID " +
                                            "INNER JOIN ParameterValues pv2 on p.priorityID = pv2.parameterValueID " +
                                            "INNER JOIN ParameterValues pv3 on p.typeID = pv3.parameterValueID " +
                                            "INNER JOIN ParameterValues pv4 on p.costCenterID = pv4.parameterValueID " +
                                            "INNER JOIN ParameterValues pv5 on p.offerID = pv5.parameterValueID " +
                                            "INNER JOIN ParameterValues pv6 on p.statusID = pv6.parameterValueID " +
                                            "INNER JOIN Users u1 on p.saleManagerID = u1.userID " +
                                            "INNER JOIN Users u2 on p.ownerID = u2.userID " +
                                            "INNER JOIN Users u3 on p.preSalesID = u3.userID " +
                                            "INNER JOIN Users u4 on p.operationManagerID = u4.userID " +
                                            "WHERE 1 = 1  ");

                if (!string.IsNullOrEmpty(command.CustomerID))
                    sql += string.Format("AND p.customerID LIKE '{0}' ", command.CustomerID);

                if (!string.IsNullOrEmpty(command.PriorityID))
                    sql += string.Format("AND p.priorityID = '{0}' ", command.PriorityID);

                if (!string.IsNullOrEmpty(command.FaseID))
                    sql += string.Format("AND p.faseID = '{0}' ", command.FaseID);

                if (!string.IsNullOrEmpty(command.OwnerID))
                    sql += string.Format("AND p.ownerID = '{0}' ", command.OwnerID);

                if (!string.IsNullOrEmpty(command.SaleManagerID))
                    sql += string.Format("AND p.saleManagerID = '{0}' ", command.SaleManagerID);

                if (!string.IsNullOrEmpty(command.OperationManagerID))
                    sql += string.Format("AND p.operationManagerID = '{0}' ", command.OperationManagerID);

                if (!string.IsNullOrEmpty(command.TypeID))
                    sql += string.Format("AND p.typeID = '{0}' ", command.TypeID);

                if (!string.IsNullOrEmpty(command.CostCenterID))
                    sql += string.Format("AND p.costCenterID = '{0}' ", command.CostCenterID);
                        
                if (!string.IsNullOrEmpty(command.OfferID))
                    sql += string.Format("AND p.offerID = '{0}' ", command.OfferID);

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND p.statusID = '{0}' ", command.StatusID);

                if (!string.IsNullOrEmpty(command.PreSalesID))
                    sql += string.Format("AND p.preSalesID = '{0}' ", command.PreSalesID);


                sql += "ORDER BY c.customerName, Convert(datetime, p.creationDate, 103) DESC";

                return conn.Query<Pipeline>(sql).ToList();
            }
        }

        public List<Pipeline> GetAllCodeByCustomerID(string customerID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = null;

                if (customerID == "0")
                {
                    sql = string.Format("Select p.oportunityID,  p.oportunityCode + ' - ' + p.summary as oportunityCode " +
                                                "From Pipelines p " +
                                                "INNER JOIN Customers c on p.customerID = c.customerID " +
                                                "INNER JOIN ParameterValues pv on p.offerID = pv.parameterValueID " +
                                                "Where p.customerID <> '{0}' ", customerID);
                }
                else
                {
                    sql = string.Format("Select p.oportunityID,  p.oportunityCode + ' - ' + p.summary as oportunityCode " +
                                          "From Pipelines p " +
                                          "INNER JOIN Customers c on p.customerID = c.customerID " +
                                          "INNER JOIN ParameterValues pv on p.offerID = pv.parameterValueID " +
                                          "Where p.customerID = '{0}' ", customerID);
                }
                sql += "ORDER BY p.oportunityID DESC";

                return conn.Query<Pipeline>(sql).ToList();
            }
        }

        public void Delete(int oportunityID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Pipelines WHERE oportunityID = '{0}'", oportunityID);
                conn.ExecuteScalar(sql);
            }
        }

        public string GetOportunityCodeByID(int oportunityID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT oportunityCode FROM dbo.Pipelines WHERE oportunityID = {0}", oportunityID);

                return conn.Query<string>(sql).FirstOrDefault();
            }
        }

    }
}
