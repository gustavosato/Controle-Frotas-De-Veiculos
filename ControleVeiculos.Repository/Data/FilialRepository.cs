using ControleVeiculos.Domain.Entities.Filiais;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Filiais;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class FilialRepository : BaseRepository, IFilialRepository
    {
        public void Add(Filial filial)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(filialID AS INT))+1,1) FROM dbo.Filials");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                FilialDapper filialDapper = filial.Map(primaryKey);

                //conn.Insert<FilialDapper>(filialDapper);

                try
                {
                    conn.Insert<FilialDapper>(filialDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Filial filial)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                FilialDapper filialDapper = filial.Map(filial.filialID);

                conn.Update<FilialDapper>(filialDapper);
            }
        }

        public Filial GetByID(int filialID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Filials WHERE filialID = '{0}'", filialID);

                return conn.Query<Filial>(sql).FirstOrDefault();
            }
        }

        public List<Filial> GetAll(FilterFilialCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT filialID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM Filials tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                //if (!string.IsNullOrEmpty(command.StatusID))
                //    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY filialID";
                return conn.Query<Filial>(sql).ToList();
            }
        }

        public void Delete(int filialID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Filials WHERE filialID = '{0}'", filialID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
