using Lean.Test.Cloud.Domain.Entities.Skills;
using Lean.Test.Cloud.MVC.Models.Skills;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class SkillMappingExtensions
    {
        public static SkillModel ToModel(this Skill entity)
        {
            if (entity == null)
                return null;

            var model = new SkillModel
            {
                SkillID = entity.skillID,
                Summary = entity.summary,
                SkillTypeID = entity.skillTypeID,
                Description = entity.description,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}