using ControleVeiculos.Domain.Entities.Kilometragems;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Kilometragems;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class KilometragemRepository : BaseRepository, IKilometragemRepository
    {
        public void Add(Kilometragem kilometragem)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(logID AS INT))+1,1) FROM dbo.Kilometragems");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                KilometragemDapper kilometragemDapper = kilometragem.Map(primaryKey);

                //conn.Insert<KilometragemDapper>(kilometragemDapper);

                try
                {
                    conn.Insert<KilometragemDapper>(kilometragemDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Kilometragem kilometragem)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                KilometragemDapper kilometragemDapper = kilometragem.Map(kilometragem.logID);

                conn.Update<KilometragemDapper>(kilometragemDapper);
            }
        }

        public Kilometragem GetByID(int logID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Kilometragems WHERE logID = '{0}'", logID);

                return conn.Query<Kilometragem>(sql).FirstOrDefault();
            }
        }

        public List<Kilometragem> GetAll(FilterKilometragemCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT logID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM Kilometragems tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY logID";
                return conn.Query<Kilometragem>(sql).ToList();
            }
        }

        public void Delete(int logID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Kilometragems WHERE logID = '{0}'", logID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
