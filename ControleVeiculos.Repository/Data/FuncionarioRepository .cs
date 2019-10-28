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
using System;

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
                try { 
                conn.Insert<FuncionarioDapper>(funcionarioDapper);
                }catch(Exception ex)
                {
                    var mensagem = ex.Message;
                }
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

                string sql = string.Format("SELECT f.funcionarioID, f.nomeFuncionario, f.endereco, f.funcao, f.setor, f.telefone " +
                                           "FROM Funcionarios f " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.NomeFuncionario))
                    sql += string.Format("AND f.nomeFuncionario LIKE '%{0}%'", command.NomeFuncionario);

                if (!string.IsNullOrEmpty(command.CPF))
                    sql += string.Format("AND f.cpf LIKE '%{0}%'", command.CPF);

                if (!string.IsNullOrEmpty(command.Setor))
                    sql += string.Format("AND f.setor LIKE '%{0}%'", command.Setor);

                if (!string.IsNullOrEmpty(command.Funcao))
                    sql += string.Format("AND f.funcao LIKE '%{0}%'", command.Funcao);


                sql += "ORDER BY f.nomeFuncionario";

                return conn.Query<Funcionario>(sql).ToList();
            }
        }

        
        //public List<Funcionario> GetAll(int funcionarioID)
        //{
        //    using (IDbConnection conn = new SqlConnection(ConnectionString))
        //    {
        //        conn.ConnectionString = this.ConnectionString;
        //        conn.Open();

        //        string sql = string.Format("SELECT * FROM Funcionarios Where 1 = 1");
                                           

        //        sql += "ORDER BY funcionarioName";

        //        return conn.Query<Funcionario>(sql).ToList();
        //    }
        //}

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
