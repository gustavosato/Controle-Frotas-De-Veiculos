using ControleVeiculos.Domain.Entities.Departamentos;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Departamentos;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class DepartamentoRepository : BaseRepository, IDepartamentoRepository
    {
        public void Add(Departamento departamento)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(logID AS INT))+1,1) FROM dbo.Departamentos");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                DepartamentoDapper departamentoDapper = departamento.Map(primaryKey);

                //conn.Insert<DepartamentoDapper>(departamentoDapper);

                try
                {
                    conn.Insert<DepartamentoDapper>(departamentoDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Departamento departamento)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                DepartamentoDapper departamentoDapper = departamento.Map(departamento.logID);

                conn.Update<DepartamentoDapper>(departamentoDapper);
            }
        }

        public Departamento GetByID(int logID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Departamentos WHERE logID = '{0}'", logID);

                return conn.Query<Departamento>(sql).FirstOrDefault();
            }
        }

        public List<Departamento> GetAll(FilterDepartamentoCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT logID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM Departamentos tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY logID";
                return conn.Query<Departamento>(sql).ToList();
            }
        }

        public void Delete(int logID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Departamentos WHERE logID = '{0}'", logID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
