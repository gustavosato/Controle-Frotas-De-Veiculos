using Lean.Test.Cloud.Domain.Command.Skills;
using Lean.Test.Cloud.Domain.Entities.Skills;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface ISkillService : IDisposable
    {
        void Add(MaintenanceSkillCommand command);
        void Update(MaintenanceSkillCommand command);
        Result<Skill> GetByID(int skillID);
        IPagedList<Skill> GetAll(FilterSkillCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int skillID);
    }
}
