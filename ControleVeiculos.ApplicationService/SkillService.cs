using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Skills;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Skills;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;


namespace ControleVeiculos.ApplicationService
{
    public class SkillService : BaseAppService, ISkillService
    {
        private readonly ISkillRepository _skillRepository;

        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public void Add(MaintenanceSkillCommand command)
        {
            Skill skill = new Skill();

            skill = skill.Map(command);

            _skillRepository.Add(skill);
        }

        public void Update(MaintenanceSkillCommand command)
        {
            Skill skill = new Skill();

            skill = skill.Map(command);

            _skillRepository.Update(skill);
        }

        public Result<Skill> GetByID(int skillID)
        {
            var skill = _skillRepository.GetByID(skillID);

            return Result.Ok<Skill>(0, "", skill);
        }

        public IPagedList<Skill> GetAll(FilterSkillCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var skill = _skillRepository.GetAll(command);

            return new PagedList<Skill>(skill, pageIndex, pageSize);
        }

        public void Delete(int skillID)
        {
            _skillRepository.Delete(skillID);
        }
    }
}

