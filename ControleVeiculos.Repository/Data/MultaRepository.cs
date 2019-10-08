﻿using ControleVeiculos.Domain.Entities.Multas;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Multas;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class MultaRepository : BaseRepository, IMultaRepository
    {
        public void Add(Multa multa)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(logID AS INT))+1,1) FROM dbo.Multas");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                MultaDapper multaDapper = multa.Map(primaryKey);

                //conn.Insert<MultaDapper>(multaDapper);

                try
                {
                    conn.Insert<MultaDapper>(multaDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(Multa multa)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                MultaDapper multaDapper = multa.Map(multa.logID);

                conn.Update<MultaDapper>(multaDapper);
            }
        }

        public Multa GetByID(int logID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Multas WHERE logID = '{0}'", logID);

                return conn.Query<Multa>(sql).FirstOrDefault();
            }
        }

        public List<Multa> GetAll(FilterMultaCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT logID, pv.parameterValue AS statusID, tl.stepName, tl.expectedResult, tl.actualResult, tl.pathEvidence " +
                                           "FROM Multas tl " +
                                           "INNER JOIN ParameterValues pv ON tl.statusID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.StatusID))
                    sql += string.Format("AND tl.statusID LIKE '%{0}%' ", command.StatusID);

                sql += "ORDER BY logID";
                return conn.Query<Multa>(sql).ToList();
            }
        }

        public void Delete(int logID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Multas WHERE logID = '{0}'", logID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}