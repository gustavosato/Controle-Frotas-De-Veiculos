using Lean.Test.Cloud.Domain.Command.Skills;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Skills
{
    public static class SkillExtensions
    {
        public static Result<Skill> GetSkill(this Skill skill)
        {
            return Result.Ok(0, "", skill);
        }

        public static Skill Map(this Skill skill, MaintenanceSkillCommand command)
        {

            skill.skillID = command.SkillID;
            skill.summary = command.Summary;
            skill.skillTypeID = command.SkillTypeID;
            skill.description = command.Description;
            skill.createdByID = command.CreatedByID;
            skill.creationDate = command.CreationDate;
            skill.modifiedByID = command.ModifiedByID;
            skill.lastModifiedDate = DateTime.Now.ToString();

            return skill;
        }
    }
}
