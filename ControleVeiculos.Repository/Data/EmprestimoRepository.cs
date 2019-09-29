using ControleVeiculos.Domain.Entities.Emprestimos;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Emprestimos;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class EmprestimoRepository : BaseRepository, IEmprestimoRepository
    {
        public void Add(Emprestimo emprestimo)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(logID AS INT))+1,1) FROM dbo.Emprestimos");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                EmprestimoDapper emprestimoDapper = emprestimo.Map(primaryKey);

                //conn.Insert<EmprestimoDapper>(emprestimoDapper);

                try
                {
                    conn.Insert<EmprestimoDapper>(emprestimoDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Emprestimo emprestimo)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                EmprestimoDapper emprestimoDapper = emprestimo.Map(emprestimo.logID);

                conn.Update<EmprestimoDapper>(emprestimoDapper);
            }
        }

        public Emprestimo GetByID(int logID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Emprestimos WHERE logID = '{0}'", logID);

                return conn.Query<Emprestimo>(sql).FirstOrDefault();
            }
        }

        public List<Emprestimo> GetAll(FilterEmprestimoCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT logID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM Emprestimos tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY logID";
                return conn.Query<Emprestimo>(sql).ToList();
            }
        }

        public void Delete(int logID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Emprestimos WHERE logID = '{0}'", logID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
