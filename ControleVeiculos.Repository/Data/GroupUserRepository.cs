using Lean.Test.Cloud.Domain.Entities.GroupsUsers;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using System;
using Lean.Test.Cloud.Domain.Command.Groups;
using Lean.Test.Cloud.Domain.Entities.Groups;

namespace Lean.Test.Cloud.Repository.Data
{
    public class GroupUserRepository : BaseRepository, IGroupUserRepository
    {
        public void Add(GroupUser groupUser)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                GroupUserDapper groupUserDapper = groupUser.Map();
                    conn.Insert<GroupUserDapper>(groupUserDapper);
            }
        }

        public void Delete(int groupID, int userID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.GroupsUsers WHERE groupID = '{0}' AND userID = '{1}'", groupID, userID);

                conn.ExecuteScalar(sql);
            }
        }

        public List<Group> GetAllAssociateGroupByUserID(FilterGroupCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT DISTINCT g.groupID, g.groupName " +
                                           "FROM Groups g " +
                                           "LEFT JOIN GroupsUsers cs ON g.groupID = cs.groupID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.GroupName))
                    sql += string.Format("AND g.groupName LIKE '%{0}%' ", command.GroupName);

                if (!string.IsNullOrEmpty(command.UserID))
                    sql += string.Format("AND cs.userID = '{0}' ", command.UserID);

                return conn.Query<Group>(sql).ToList();
            }
        }

        public List<Group> GetAllNoAssociateGroupByUserID(FilterGroupCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT DISTINCT g.groupID, g.groupName " +
                                           "FROM Groups g " +
                                           "LEFT JOIN GroupsUsers cs ON g.groupID = cs.groupID " +
                                           "WHERE g.groupID NOT IN (SELECT DISTINCT g.groupID FROM Groups g " +
                                           "INNER JOIN GroupsUsers cs ON g.groupID = cs.groupID " +
                                           "WHERE cs.userID = '{0}' ) ", command.UserID);

                if (!string.IsNullOrEmpty(command.GroupName))
                    sql += string.Format("AND g.groupName LIKE '%{0}%' ", command.GroupName);

                sql += "ORDER BY g.groupName";
                return conn.Query<Group>(sql).ToList();
            }
        }

    }
}
