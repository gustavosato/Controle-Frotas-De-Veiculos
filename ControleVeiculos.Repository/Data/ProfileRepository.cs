using Lean.Test.Cloud.Domain.Entities.Profiles;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.Profiles;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class ProfileRepository : BaseRepository, IProfileRepository
    {
        public void Add(Profile Profile)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(profileID AS INT))+1,1) FROM dbo.Profiles");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                ProfileDapper profileDapper = Profile.Map(primaryKey);

                // conn.Insert<ProfileDapper>(profileDapper);

                try
                {
                    conn.Insert<ProfileDapper>(profileDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Profile profile)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                ProfileDapper profileDapper = profile.Map(profile.ProfileID);

                conn.Update<ProfileDapper>(profileDapper);
            }
        }

        public Profile GetByID(int profileID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Profiles WHERE profileID = '{0}'", profileID);

                return conn.Query<Profile>(sql).FirstOrDefault();
            }
        }

        public string GetAllow(FilterProfileCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("select count(*) " +
                                            "from Profiles p " +
                                            "inner join GroupsUsers gu on p.groupID = gu.groupID " +
                                            "Where 1 = 1 ");

                if (command.AllowAdd)
                    sql += string.Format("AND p.allowAdd = '{0}' ", command.AllowAdd);

                if (command.AllowAddRemove)
                    sql += string.Format("AND p.allowAddRemove = '{0}' ", command.AllowAddRemove);

                if (command.AllowChangeStatus)
                    sql += string.Format("AND p.allowChangeStatus = '{0}' ", command.AllowChangeStatus);

                if (command.AllowDelete)
                    sql += string.Format("AND p.allowDelete = '{0}' ", command.AllowDelete);

                if (command.AllowExportExcel)
                    sql += string.Format("AND p.allowExportExcel = '{0}' ", command.AllowExportExcel);

                if (command.AllowReportView)
                    sql += string.Format("AND p.allowReportView = '{0}' ", command.AllowReportView);

                if (command.AllowUpdate)
                    sql += string.Format("AND p.allowUpdate = '{0}' ", command.AllowUpdate);

                if (command.AllowView)
                    sql += string.Format("AND p.allowView = '{0}' ", command.AllowView);

                if (!string.IsNullOrEmpty(command.UserID))
                    sql += string.Format("AND gu.userID = '{0}' ", command.UserID);

                if (!string.IsNullOrEmpty(command.SystemFeatureID))
                    sql += string.Format("AND p.SystemFeatureID = '{0}' ", command.SystemFeatureID);

                return conn.Query<string>(sql).FirstOrDefault();
            }
        }

        public List<Profile> GetAll(FilterProfileCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT profileID, g.groupName as groupID, s.systemFeatureName as systemFeatureID, allowView, allowAdd, allowUpdate, allowDelete, allowChangeStatus, allowAddRemove, allowExportExcel, allowReportView " +
                                           "FROM Profiles as p "+
                                           "INNER JOIN Groups as g on p.groupID = g.groupID "+
                                           "INNER JOIN SystemFeatures as s on p.systemFeatureID = s.systemFeatureID " + 
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.GroupID))
                    sql += string.Format("AND p.groupID = '{0}' ", command.GroupID);

                if (!string.IsNullOrEmpty(command.SystemFeatureID))
                    sql += string.Format("AND p.systemFeatureID = '{0}' ", command.SystemFeatureID);

                sql += "ORDER BY p.profileID ";
                return conn.Query<Profile>(sql).ToList();
            }
        }



        public void Delete(int profileID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Profiles WHERE profileID = '{0}'", profileID);

                conn.ExecuteScalar(sql);
            }
        }

    }
}
