using ControleVeiculos.Domain.Entities.SystemMenus;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.SystemMenus;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class SystemMenuRepository : BaseRepository, ISystemMenuRepository
    {
        public void Add(SystemMenu systemMenu)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(menuID AS INT))+1,1) FROM dbo.SystemMenus");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                SystemMenuDapper systemMenuDapper = systemMenu.Map(primaryKey);

                //conn.Insert<SystemMenuDapper>(systemMenuDapper);

                try
                {
                    conn.Insert<SystemMenuDapper>(systemMenuDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(SystemMenu systemMenu)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                SystemMenuDapper systemMenuDapper = systemMenu.Map(systemMenu.menuID);

                conn.Update<SystemMenuDapper>(systemMenuDapper);
            }
        }

        public SystemMenu GetByID(int menuID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.SystemMenus WHERE menuID = '{0}'", menuID);

                return conn.Query<SystemMenu>(sql).FirstOrDefault();
            }
        }

        public List<SystemMenu> GetAll(FilterSystemMenuCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT menuID, textMenu, sf.systemFeatureName AS systemFeatureID, urlAction, controller, ordem " +
                                           "FROM SystemMenus sm " +
                                           "LEFT JOIN SystemFeatures sf ON sf.systemFeatureID = sm.systemFeatureID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.SystemFeatureID))
                    sql += string.Format("AND sm.systemFeatureID = '{0}' ", command.SystemFeatureID);

                sql += "ORDER BY sm.systemFeatureID";
                return conn.Query<SystemMenu>(sql).ToList();
            }
        }

        public List<SystemMenu> GetAll(int menuID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT menuID, textMenu, sf.systemFeatureName AS systemFeatureID, urlAction, controller, ordem " +
                                           "FROM SystemMenus sm " +
                                           "LEFT JOIN SystemFeatures sf ON sf.systemFeatureID = sm.systemFeatureID " +
                                           "WHERE 1 = 1 ");

                sql += "ORDER BY sm.systemFeatureID";
                return conn.Query<SystemMenu>(sql).ToList();
            }
        }


        public void Delete(int menuID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.SystemMenus WHERE menuID = '{0}'", menuID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
