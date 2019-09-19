using Lean.Test.Cloud.Domain.Entities.Defects;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.Defects;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class DefectRepository : BaseRepository, IDefectRepository
    {
        public string Add(Defect Defect)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(defectID AS INT))+1,1) FROM dbo.Defects");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                DefectDapper defectDapper = Defect.Map(primaryKey);

                try
                {
                conn.Insert<DefectDapper>(defectDapper);
                
                    return primaryKey.ToString();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public void Update(Defect defect)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                DefectDapper defectDapper = defect.Map(defect.defectID);

                conn.Update<DefectDapper>(defectDapper);
            }
        }

        public Defect GetByID(int defectID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Defects WHERE defectID = '{0}'", defectID);

                return conn.Query<Defect>(sql).FirstOrDefault();
            }
        }

        public List<Defect> GetAll(FilterDefectCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT defectID, summary, pv.parameterValue as statusID, " +
                                            "pv1.parameterValue as severityID, pv2.parameterValue as priorityID, " +
                                            "u.userName as assingToID, u1.userName as createdByID, d.creationDate " +
                                            "FROM Defects d " +
                                            "INNER JOIN ParameterValues pv on d.statusID = pv.parameterValueID " +
                                            "INNER JOIN ParameterValues pv1 on d.severityID = pv1.parameterValueID " +
                                            "INNER JOIN ParameterValues pv2 on d.priorityID = pv2.parameterValueID " +
                                            "LEFT JOIN Users u on d.assingToID = u.userID " +
                                            "INNER JOIN Users u1 on d.createdByID = u1.userID " +
                                            "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.Summary))
                    sql += string.Format("AND d.summary LIKE '%{0}%'", command.Summary);

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND d.statusID = '{0}'", command.StatusID);

                if (!string.IsNullOrEmpty(command.SeverityID))
                    sql += string.Format("AND d.severityID = '{0}'", command.SeverityID);

                if (!string.IsNullOrEmpty(command.PriorityID))
                    sql += string.Format("AND d.priorityID = '{0}'", command.PriorityID);

                if (!string.IsNullOrEmpty(command.AssingToID))
                    sql += string.Format("AND d.assingToID = '{0}'", command.AssingToID);

                if (!string.IsNullOrEmpty(command.CreatedByID))
                    sql += string.Format("AND d.createdByID = '{0}'", command.CreatedByID);

                sql += "ORDER BY Convert(datetime, d.creationDate, 103) Desc";

                return conn.Query<Defect>(sql).ToList();
            }
        }

        public void Delete(int defectID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Defects WHERE defectID = '{0}'", defectID);
                conn.ExecuteScalar(sql);
            }
        }

        public List<Defect> ApiGetAll()
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                //string sql = string.Format("SELECT * FROM Defects d WHERE 1 =1 ");

                string sql = string.Format("SELECT defectID, summary, "+
                                            "pv.parameterValue as statusID, " +
                                            "pv1.parameterValue as severityID, pv2.parameterValue as priorityID, " +
                                            "u.userName as assingToID, u1.userName as createdByID, d.creationDate " +
                                            "FROM Defects d " +
                                            "LEFT JOIN ParameterValues pv on d.statusID = pv.parameterValueID " +
                                            "LEFT JOIN ParameterValues pv1 on d.severityID = pv1.parameterValueID " +
                                            "LEFT JOIN ParameterValues pv2 on d.priorityID = pv2.parameterValueID " +
                                            "LEFT JOIN Users u on d.assingToID = u.userID " +
                                            "INNER JOIN Users u1 on d.createdByID = u1.userID " +
                                            "WHERE 1 = 1 ");

                sql += "ORDER BY Convert(datetime, d.creationDate, 103) Desc";
                //sql += "ORDER BY d.defectID Desc";

                return conn.Query<Defect>(sql).ToList();
            }
        }
    }
}
