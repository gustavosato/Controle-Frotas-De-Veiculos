using Lean.Test.Cloud.Domain.Entities.PipelineEvents;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.PipelineEvents;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class PipelineEventRepository : BaseRepository, IPipelineEventRepository
    {
        public void Add(PipelineEvent pipelineEvent)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(saleEventID AS INT))+1,1) FROM dbo.PipelineEvents");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                PipelineEventDapper pipelineEventDapper = pipelineEvent.Map(primaryKey);

                try
                {
                    conn.Insert<PipelineEventDapper>(pipelineEventDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }

                //conn.Insert<PipelineEventDapper>(pipelineEventDapper);
            }
        }

        public void Update(PipelineEvent pipelineEvent)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();


                PipelineEventDapper pipelineEventDapper = pipelineEvent.Map(pipelineEvent.saleEventID);

                conn.Update<PipelineEventDapper>(pipelineEventDapper);
            }
        }

        public PipelineEvent GetByID(int saleEventID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.PipelineEvents WHERE saleEventID = '{0}'", saleEventID);

                return conn.Query<PipelineEvent>(sql).FirstOrDefault();
            }
        }

        public List<PipelineEvent> GetAll(FilterPipelineEventCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT c.customerName + '/' + p.summary + ' - ' + p.oportunityCode as oportunityID, pe.saleEventID, pe.summary, pe.registerDate, pv1.parameterValue as typeID, " +
                                            "pv2.parameterValue as nextStepID, pe.targetDate, pe.description, " +
                                            "u.userName as createdByID, pe.creationDate " +
                                            "FROM PipelineEvents pe " +
                                            "INNER JOIN Pipelines p on pe.oportunityID = p.oportunityID " +
                                            "INNER JOIN Customers c on p.customerID = c.customerID " +
                                            "INNER JOIN ParameterValues pv1 on pe.typeID = pv1.parameterValueID " +
                                            "INNER JOIN ParameterValues pv2 on pe.nextStepID = pv2.parameterValueID " +
                                            "INNER JOIN Users u on pe.createdByID = u.userID " +
                                            "WHERE 1 = 1  ");

                if (!string.IsNullOrEmpty(command.RegisterDate))
                    sql += string.Format("AND pe.registerDate = '{0}' ", command.RegisterDate);

                if (!string.IsNullOrEmpty(command.TypeID))
                    sql += string.Format("AND pe.typeID = '{0}' ", command.TypeID);

                if (!string.IsNullOrEmpty(command.NextStepID))
                    sql += string.Format("AND pe.nextStepID = '{0}' ", command.NextStepID);

                if (!string.IsNullOrEmpty(command.OportunityID))
                    sql += string.Format("AND pe.oportunityID = '{0}' ", command.OportunityID);

                if (!string.IsNullOrEmpty(command.Description))
                    sql += string.Format("AND pe.description LIKE '%{0}%' ", command.Description);

                if (!string.IsNullOrEmpty(command.CreatedBy))
                    sql += string.Format("AND pe.createdByID = '{0}' ", command.CreatedBy);

                sql += "Order By Convert(datetime, pe.RegisterDate, 103) DESC";

                return conn.Query<PipelineEvent>(sql).ToList();
            }
        }

        public void Delete(int saleEventID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.PipelineEvents WHERE saleEventID = '{0}'", saleEventID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
