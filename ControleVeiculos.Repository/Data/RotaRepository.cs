﻿using ControleVeiculos.Domain.Entities.Rotas;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Rotas;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class RotaRepository : BaseRepository, IRotaRepository
    {
        public void Add(Rota rota)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(logID AS INT))+1,1) FROM dbo.Rotas");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                RotaDapper rotaDapper = rota.Map(primaryKey);

                //conn.Insert<RotaDapper>(rotaDapper);

                try
                {
                    conn.Insert<RotaDapper>(rotaDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Rota rota)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                RotaDapper rotaDapper = rota.Map(rota.logID);

                conn.Update<RotaDapper>(rotaDapper);
            }
        }

        public Rota GetByID(int logID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Rotas WHERE logID = '{0}'", logID);

                return conn.Query<Rota>(sql).FirstOrDefault();
            }
        }

        public List<Rota> GetAll(FilterRotaCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT logID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM Rotas tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY logID";
                return conn.Query<Rota>(sql).ToList();
            }
        }

        public void Delete(int logID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Rotas WHERE logID = '{0}'", logID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}