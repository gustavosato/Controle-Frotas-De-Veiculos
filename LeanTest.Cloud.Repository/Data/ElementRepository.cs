using Lean.Test.Cloud.Domain.Entities.Elements;
using Lean.Test.Cloud.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Lean.Test.Cloud.Repository.Map;
using Dapper.Contrib.Extensions;
using Lean.Test.Cloud.Domain.Command.Elements;

namespace Lean.Test.Cloud.Repository.Data
{
    public class ElementRepository : BaseRepository, IElementRepository
    {
        public void Add(Element element)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(elementID AS INT))+1,1) FROM dbo.Elements");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                ElementDapper elementDapper = element.Map(primaryKey);

                conn.Insert<ElementDapper>(elementDapper);
            }
        }

        public void Update(Element element)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                ElementDapper elementDapper = element.Map(element.elementID);

                conn.Update<ElementDapper>(elementDapper);
            }
        }

        public Element GetByID(int elementID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Elements WHERE elementID = '{0}'", elementID);

                return conn.Query<Element>(sql).FirstOrDefault();
            }
        }

        public List<Element> GetAll(FilterElementCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT * FROM Elements WHERE 1 =  1 ");

                if (!string.IsNullOrEmpty(command.FeatureID))
                    sql += string.Format("AND featureID = {0}" , command.FeatureID);

                sql += "ORDER BY elementID";
                return conn.Query<Element>(sql).ToList();
            }
        }

        public void Delete(int elementID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Element WHERE elementID = '{0}'", elementID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
