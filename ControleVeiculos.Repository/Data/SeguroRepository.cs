using ControleVeiculos.Domain.Entities.Seguros;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Seguros;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class SeguroRepository : BaseRepository, ISeguroRepository
    {
        public void Add(Seguro seguro)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(seguroID AS INT))+1,1) FROM dbo.Seguro");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                SeguroDapper seguroDapper = seguro.Map(primaryKey);

                //conn.Insert<SeguroDapper>(seguroDapper);

                try
                {
                    conn.Insert<SeguroDapper>(seguroDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Seguro seguro)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                SeguroDapper seguroDapper = seguro.Map(seguro.seguroID);

                conn.Update<SeguroDapper>(seguroDapper);
            }
        }

        public Seguro GetByID(int seguroID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Seguro WHERE seguroID = '{0}'", seguroID);

                return conn.Query<Seguro>(sql).FirstOrDefault();
            }
        }

        public List<Seguro> GetAll(FilterSeguroCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT se.seguroID, v.modelo AS veiculoID, se.apolice, se.tipoSeguro, se.dataContratacao, " +
                                           "se.vigencia, se.fimContratacao " +
                                           "FROM Seguro se " +
                                           "INNER JOIN Veiculos v ON se.veiculoID = v.veiculoID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.Apolice))
                    sql += string.Format("AND se.apolice LIKE '%{0}%' ", command.Apolice);

                if (!string.IsNullOrEmpty(command.VeiculoID))
                    sql += string.Format("AND se.veiculoID = '{0}' ", command.VeiculoID);

                if (!string.IsNullOrEmpty(command.Seguradora))
                    sql += string.Format("AND se.seguradora LIKE '%{0}%' ", command.Seguradora);

                if (!string.IsNullOrEmpty(command.Franquia))
                    sql += string.Format("AND se.franquia LIKE '%{0}%' ", command.Franquia);

                if (!string.IsNullOrEmpty(command.TipoSeguro))
                    sql += string.Format("AND se.tipoSeguro LIKE '%{0}%' ", command.TipoSeguro);

                if (!string.IsNullOrEmpty(command.DataContratacao))
                    sql += string.Format("AND se.dataContratacao LIKE '%{0}%' ", command.DataContratacao);

                if (!string.IsNullOrEmpty(command.Vigencia))
                    sql += string.Format("AND se.vigencia LIKE '%{0}%' ", command.Vigencia);

                if (!string.IsNullOrEmpty(command.FimContratacao))
                    sql += string.Format("AND se.fimContratacao LIKE '%{0}%' ", command.FimContratacao);

                sql += "ORDER BY seguroID";
                return conn.Query<Seguro>(sql).ToList();
            }
        }

        public void Delete(int seguroID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Seguro WHERE seguroID = '{0}'", seguroID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
