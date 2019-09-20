using ControleVeiculos.Domain.Command.Skills;
using ControleVeiculos.Domain.Entities.Skills;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface ISkillRepository
    {
        void Add(Skill skill);
        void Update(Skill skill);
        Skill GetByID(int skill);
        List<Skill> GetAll(FilterSkillCommand command);
        void Delete(int skillID);
    }
}
