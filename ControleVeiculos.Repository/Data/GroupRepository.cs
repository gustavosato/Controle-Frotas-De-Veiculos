using Lean.Test.Cloud.Domain.Entities.Groups;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.Groups;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class GroupRepository : BaseRepository, IGroupRepository
    {
        public void Add(Group Group)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(groupID AS INT))+1,1) FROM dbo.Groups");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                GroupDapper groupDapper = Group.Map(primaryKey);
                try
                {
                    conn.Insert<GroupDapper>(groupDapper);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Group group)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                GroupDapper groupDapper = group.Map(group.groupID);

                conn.Update<GroupDapper>(groupDapper);
            }
        }

        public Group GetByID(int groupID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Groups WHERE groupID = '{0}'", groupID);

                return conn.Query<Group>(sql).FirstOrDefault();
            }
        }

        public List<Group> GetAll(FilterGroupCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();


                string sql = string.Format("SELECT groupID, groupName, g.description " +
                                           "FROM Groups g " +
                                           "WHERE 1 = 1 ");


                if (!string.IsNullOrEmpty(command.GroupName))
                    sql += string.Format("AND g.groupName LIKE '%{0}%'", command.GroupName);

                sql += "ORDER BY groupName";

                return conn.Query<Group>(sql).ToList();
            }
        }

        public List<Group> GetAll(int groupID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Groups  ");

                sql += "ORDER BY groupName";

                return conn.Query<Group>(sql).ToList();
            }
        }

        public void Delete(int groupID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Groups WHERE groupID = '{0}'", groupID);
                conn.ExecuteScalar(sql);
            }
        }

       public string GetGroupNameByID(int groupID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT groupName FROM dbo.Groups WHERE groupID = {0}", groupID);

                return conn.Query<string>(sql).FirstOrDefault();

            }
        }
    }
}
