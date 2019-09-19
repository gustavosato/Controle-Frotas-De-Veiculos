using Lean.Test.Cloud.Domain.Entities.Attachments;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.Attachments;
using System;
using System.IO;

namespace Lean.Test.Cloud.Repository.Data
{
    public class AttachmentRepository : BaseRepository, IAttachmentRepository
    {
        public void Add(Attachment attachment)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(attachmentID AS INT))+1,1) FROM dbo.Attachments");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                AttachmentDapper attachmentDapper = attachment.Map(primaryKey);

                try
                {
                    conn.Insert<AttachmentDapper>(attachmentDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Attachment attachment)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                AttachmentDapper attachmentDapper = attachment.Map(attachment.attachmentID);

                conn.Update<AttachmentDapper>(attachmentDapper);
            }
        }

        public Attachment GetByID(int AttachmentID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Attachments WHERE attachmentID = '{0}' ", AttachmentID);

                return conn.Query<Attachment>(sql).FirstOrDefault();
            }
        }

        public List<Attachment> GetAll(FilterAttachmentCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT a.attachmentID, a.fileName, a.description, a.pathFile, a.sizeFile, a.recordID, " +
                                            "s.systemFeatureName as systemFeatureID, u.userName as createdByID, a.creationDate " +
                                            "FROM Attachments a " +
                                            "LEFT JOIN SystemFeatures s on a.systemFeatureID = s.systemFeatureID " +
                                            "INNER JOIN Users u on a.createdByID = u.userID " +
                                            "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.FileName))
                    sql += string.Format("AND a.fileName LIKE '%{0}%' ", command.FileName);

                if (!string.IsNullOrEmpty(command.Description))
                    sql += string.Format("AND a.description LIKE '%{0}%' ", command.Description);

                if (!string.IsNullOrEmpty(command.RecordID))
                    sql += string.Format("AND a.recordID = '{0}' ", command.RecordID);

                if (!string.IsNullOrEmpty(command.SystemFeatureID))
                    sql += string.Format("AND a.SystemFeatureID = '{0}' ", command.SystemFeatureID);

                if (!string.IsNullOrEmpty(command.CreatedByID))
                    sql += string.Format("AND a.createdByID = '{0}' ", command.CreatedByID);

                sql += "ORDER BY fileName";

                return conn.Query<Attachment> (sql).ToList();
            }
        }

        public void Delete(int attachmentID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Attachments WHERE attachmentID = '{0}'", attachmentID);
                conn.ExecuteScalar(sql);
            }
        }

        public void Delete(string systemFeatureID, int recordID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT pathFile FROM dbo.Attachments Where systemFeatureID = '{0}' AND recordID = '{1}'", systemFeatureID, recordID);

                var path = conn.Query<string>(sql).ToList();

                List<string> paths =  conn.Query<string>(sql).ToList();

                foreach (string p in paths)
                {
                    var file = new FileInfo(p.ToString());

                    try
                    {
                        file.Delete();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message.ToString());
                    }
             }

            sql = string.Format("DELETE FROM dbo.Attachments WHERE systemFeatureID = '{0}' AND recordID = '{1}'", systemFeatureID, recordID);

                conn.ExecuteScalar(sql);
            }
        }
    }
}
