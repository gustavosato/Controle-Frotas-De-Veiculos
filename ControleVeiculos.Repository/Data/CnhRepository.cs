using ControleVeiculos.Domain.Entities.Cnhs;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Cnhs;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class CnhRepository : BaseRepository, ICnhRepository
    {
        public void Add(Cnh cnh)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(cnhID AS INT))+1,1) FROM dbo.Cnhs");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                CnhDapper cnhDapper = cnh.Map(primaryKey);

                conn.Insert<CnhDapper>(cnhDapper);
               
            }
        }

        public void Update(Cnh cnh)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                CnhDapper cnhDapper = cnh.Map(cnh.cnhID);

                conn.Update<CnhDapper>(cnhDapper);
            }
        }

        public Cnh GetByID(int cnhID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Cnhs WHERE cnhID = '{0}'", cnhID);

                return conn.Query<Cnh>(sql).FirstOrDefault();
            }
        }

        public List<Cnh> GetAll(FilterCnhCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ts.cnhID, ts.cnh, pv.parameterValue AS testTypeID, pv1.parameterValue AS executionTypeID, pv2.parameterValue AS statusID, " +
                                           "ts.startExecution, ts.endExecution, ts.timeExecution " +
                                           "FROM Cnhs ts " +
                                           "INNER JOIN ParameterValues pv ON ts.testTypeID = pv.parameterValueID " +
                                           "INNER JOIN ParameterValues pv1 ON ts.executionTypeID = pv1.parameterValueID " +
                                           "INNER JOIN ParameterValues pv2 ON ts.statusID = pv2.parameterValueID " +
                                           "WHERE 1 = 1 ");

                //if (!string.IsNullOrEmpty(command.StatusID))
                //    sql += string.Format("AND ts.statusID = '{0}' ", command.StatusID);

                sql += "ORDER BY cnhID";
                return conn.Query<Cnh>(sql).ToList();
            }
        }

        public void Delete(int cnhID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Cnhs WHERE cnhID = '{0}'", cnhID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
