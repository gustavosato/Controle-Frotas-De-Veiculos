using Lean.Test.Cloud.Domain.Entities.DemandsUsers;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class DemandUserRepository : BaseRepository, IDemandUserRepository
    {
        public void Add(DemandUser demandUser)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                DemandUserDapper demandUserDapper = demandUser.Map();
                    conn.Insert<DemandUserDapper>(demandUserDapper);
            }
        }

        public void Delete(int demandID, int userID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.DemandsUsers WHERE demandID = '{0}' AND userID = '{1}'", demandID, userID);

                conn.ExecuteScalar(sql);
            }
        }

    }
}
