using ControleVeiculos.Domain.Entities.Templates;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Templates;

namespace ControleVeiculos.Repository.Data
{
    public class TemplateRepository : BaseRepository, ITemplateRepository
    {
        public void Add(Template template)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(templateID AS INT))+1,1) FROM dbo.Templates");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                TemplateDapper templateDapper = template.Map(primaryKey);

                conn.Insert<TemplateDapper>(templateDapper);
            }
        }

        public void Update(Template template)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                TemplateDapper templateDapper = template.Map(template.templateID);

                conn.Update<Template>(template);
            }
        }

        public Template GetByID(int templateID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Templates WHERE templateID = '{0}'", templateID);

                return conn.Query<Template>(sql).FirstOrDefault();
            }
        }

        public List<Template> GetAll(FilterTemplateCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT * FROM Templates WHERE 1 =  1 ");

                if (!string.IsNullOrEmpty(command.TemplateName))
                    sql += string.Format("AND templateName LIKE '%{0}%' ", command.TemplateName);

                sql += "ORDER BY templateID";
                return conn.Query<Template>(sql).ToList();
            }
        }

        public void Delete(int templateID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Templates WHERE templateID = '{0}'", templateID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
