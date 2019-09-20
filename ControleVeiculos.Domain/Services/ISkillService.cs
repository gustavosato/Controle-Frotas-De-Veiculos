using ControleVeiculos.Domain.Command.Skills;
using ControleVeiculos.Domain.Entities.Skills;
using System;

namespace ControleVeiculos.Domain.Services
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
