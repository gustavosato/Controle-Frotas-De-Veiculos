using ControleVeiculos.Domain.Entities.Funcionarios;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Funcionarios;

namespace ControleVeiculos.Repository.Data
{
    public class FuncionarioRepository : BaseRepository, IFuncionarioRepository
    {
        public void Add(Funcionario Funcionario)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(funcionarioID AS INT))+1,1) FROM dbo.Funcionarios");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                FuncionarioDapper funcionarioDapper = Funcionario.Map(primaryKey);

                conn.Insert<FuncionarioDapper>(funcionarioDapper);
            }
        }

        public void Update(Funcionario funcionario)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                FuncionarioDapper funcionarioDapper = funcionario.Map(funcionario.funcionarioID);

                conn.Update<FuncionarioDapper>(funcionarioDapper);
            }
        }

        public Funcionario GetByID(int funcionarioID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Funcionarios WHERE funcionarioID = '{0}'", funcionarioID);

                return conn.Query<Funcionario>(sql).FirstOrDefault();
            }
        }

        public List<Funcionario> GetAll(FilterFuncionarioCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT v.funcionarioID, v.summary, pv.parameterValue as funcionariosTypeID, pv1.parameterValue as contractTypeID, c.customerName as customerID, " +
                                           "pv2.parameterValue as validityID, pv3.parameterValue as statusID, v.workPlace, u.userName as createdByID " +
                                           "FROM Funcionarios v " +
                                           "INNER JOIN ParameterValues pv on v.funcionariosTypeID = pv.parameterValueID " +
                                           "INNER JOIN ParameterValues pv1 on v.contractTypeID = pv1.parameterValueID " +
                                           "INNER JOIN ParameterValues pv2 on v.validityID = pv2.parameterValueID " +
                                           "INNER JOIN ParameterValues pv3 on v.statusID = pv3.parameterValueID " +
                                           "INNER JOIN Customers c on v.customerID = c.customerID " +
                                           "INNER JOIN Users u on v.createdByID = u.userID " +
                                           "WHERE 1 = 1 ");

                //if (!string.IsNullOrEmpty(command.Summary))
                //    sql += string.Format("AND v.summary LIKE '%{0}%'", command.Summary);

                
                sql += "ORDER BY v.funcionarioID";

                return conn.Query<Funcionario>(sql).ToList();
            }
        }

        
        public List<Funcionario> GetAll(int funcionarioID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT funcionarioID, summary, pv.parameterValue as funcionariosTypeID, u.userName as createdByID " +
                                           "FROM Funcionarios v " +
                                           "INNER JOIN ParameterValues pv on v.funcionariosTypeID = pv.parameterValueID " +
                                           "INNER JOIN Users u on v.createdByID = u.userID ");

                sql += "ORDER BY c.funcionarioName";

                return conn.Query<Funcionario>(sql).ToList();
            }
        }

        public void Delete(int funcionarioID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Funcionarios WHERE funcionarioID = '{0}'", funcionarioID);
                conn.ExecuteScalar(sql);
            }
        }
        public string GetFuncionarioNameByID(int contatctID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT funcionarioName FROM dbo.Funcionarios WHERE funcionarioID = {0}", contatctID);

                return conn.Query<string>(sql).FirstOrDefault();

            }
        }
    }
}
