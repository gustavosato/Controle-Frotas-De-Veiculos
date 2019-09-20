using ControleVeiculos.Domain.Entities.Skills;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Skills;

namespace ControleVeiculos.Repository.Data
{
    public class SkillRepository : BaseRepository, ISkillRepository
    {
        public void Add(Skill skill)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(skillID AS INT))+1,1) FROM dbo.Skills");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                SkillDapper skillDapper = skill.Map(primaryKey);

                conn.Insert<SkillDapper>(skillDapper);
            }
        }

        public void Update(Skill skill)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                SkillDapper skillDapper = skill.Map(skill.skillID);

                conn.Update<SkillDapper>(skillDapper);
            }
        }

        public Skill GetByID(int skillID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Skills WHERE skillID = '{0}'", skillID);

                return conn.Query<Skill>(sql).FirstOrDefault();
            }
        }

        public List<Skill> GetAll(FilterSkillCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT skillID, summary, pv.parameterValue as skillTypeID, " +
                                           "s.description, s.creationDate " +
                                           "FROM Skills s " +
                                           "INNER JOIN ParameterValues pv on s.skillTypeID = pv.parameterValueID " +
                                           "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.SkillTypeID))
                    sql += string.Format("AND s.skillTypeID = '{0}' ", command.SkillTypeID);

				if (!string.IsNullOrEmpty(command.Summary))
					sql += string.Format("AND s.summary LIKE '%{0}%' ", command.Summary);

				sql += "ORDER BY skillID";

                return conn.Query<Skill>(sql).ToList();
            }
        }

        public void Delete(int skillID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Skills WHERE skillID = '{0}'", skillID);
                conn.ExecuteScalar(sql);
            }
        }

    }
}
