using ControleVeiculos.Domain.Entities.Documentos;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Documentos;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class DocumentoRepository : BaseRepository, IDocumentoRepository
    {
        public void Add(Documento documento)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(logID AS INT))+1,1) FROM dbo.Documentos");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                DocumentoDapper documentoDapper = documento.Map(primaryKey);

                //conn.Insert<DocumentoDapper>(documentoDapper);

                try
                {
                    conn.Insert<DocumentoDapper>(documentoDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Documento documento)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                DocumentoDapper documentoDapper = documento.Map(documento.logID);

                conn.Update<DocumentoDapper>(documentoDapper);
            }
        }

        public Documento GetByID(int logID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Documentos WHERE logID = '{0}'", logID);

                return conn.Query<Documento>(sql).FirstOrDefault();
            }
        }

        public List<Documento> GetAll(FilterDocumentoCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT logID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM Documentos tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY logID";
                return conn.Query<Documento>(sql).ToList();
            }
        }

        public void Delete(int logID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Documentos WHERE logID = '{0}'", logID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
