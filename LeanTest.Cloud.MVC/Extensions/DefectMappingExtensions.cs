using Lean.Test.Cloud.Domain.Entities.Defects;
using Lean.Test.Cloud.MVC.Models.Defects;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class DefectMappingExtensions
    {
        public static DefectModel ToModel(this Defect entity)
        {
            if (entity == null)
                return null;

            var model = new DefectModel
            {
                DefectID = entity.defectID,
                Summary = entity.summary,
                Description = entity.description,
                StatusID = entity.statusID,
                SeverityID = entity.severityID,
                PriorityID = entity.priorityID,
                AssingToID = entity.assingToID,
                TypeID = entity.typeID,
                ResolutionID = entity.resolutionID,
                Resolution = entity.resolution,
                ResolutionDate = entity.resolutionDate,
                ApplicationSystemID = entity.applicationSystemID,
                FeatureID = entity.featureID,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate

            };

            return model;
        }
    }
}