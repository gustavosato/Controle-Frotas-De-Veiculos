using Lean.Test.Cloud.Domain.Entities.EquipmentAccessories;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.EquipmentAccessories;
using System;

namespace Lean.Test.Cloud.Repository.Data
{
    public class EquipmentAccessorieRepository : BaseRepository, IEquipmentAccessorieRepository
    {
        public void Add(EquipmentAccessorie equipmentAccessorie)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(equipmentAccessorieID AS INT))+1,1) FROM dbo.EquipmentAccessories");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                EquipmentAccessorieDapper equipmentAccessorieDapper = equipmentAccessorie.Map(primaryKey);

                try
                {
                    conn.Insert<EquipmentAccessorieDapper>(equipmentAccessorieDapper);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
        }

        public void Update(EquipmentAccessorie equipmentAccessorie)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                EquipmentAccessorieDapper equipmentAccessorieDapper = equipmentAccessorie.Map(equipmentAccessorie.equipmentAccessorieID);

                conn.Update<EquipmentAccessorieDapper>(equipmentAccessorieDapper);
                
            }
        }

        public EquipmentAccessorie GetByID(int equipmentAccessorieID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.EquipmentAccessories WHERE equipmentAccessorieID = '{0}'", equipmentAccessorieID);

                return conn.Query<EquipmentAccessorie>(sql).FirstOrDefault();
            }
        }

        public List<EquipmentAccessorie> GetAll(FilterEquipmentAccessorieCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT e.equipmentAccessorieID, u.userName as assigntoID, pv.parameterValue as typeID, e.serialNumber, e.startDate, e.endDate, e.modelName, " +
                                           "FORMAT(Convert(float, replace(replace(AmountInvoicing, ',', '.'), 'R$', '')), 'c', 'pt-br') as AmountInvoicing " +
                                           "FROM EquipmentAccessories e " +
                                           "INNER JOIN Users u on e.assignToID = u.userID " +
                                           "INNER JOIN ParameterValues pv on e.typeID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.AssignToID))
                    sql += string.Format("AND e.assignToID = '{0}' ", command.AssignToID);

                if (!string.IsNullOrEmpty(command.TypeID))
                    sql += string.Format("AND e.typeID = '{0}' ", command.TypeID);

                sql += "ORDER BY e.typeID";
                return conn.Query<EquipmentAccessorie>(sql).ToList();
            }
        }

        public void Delete(int equipmentAccessorieID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.EquipmentAccessories WHERE equipmentAccessorieID = '{0}'", equipmentAccessorieID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
