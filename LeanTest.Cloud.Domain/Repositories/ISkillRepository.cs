using Lean.Test.Cloud.Domain.Command.Skills;
using Lean.Test.Cloud.Domain.Entities.Skills;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
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
