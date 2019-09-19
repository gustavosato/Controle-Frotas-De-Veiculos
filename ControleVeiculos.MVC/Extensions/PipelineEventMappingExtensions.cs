using Lean.Test.Cloud.Domain.Entities.PipelineEvents;
using Lean.Test.Cloud.MVC.Models.PipelineEvents;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class PipelineEventMappingExtensions
    {
        public static PipelineEventModel ToModel(this PipelineEvent entity)
        {
            if (entity == null)
                return null;

            var model = new PipelineEventModel
            {
                SaleEventID = entity.saleEventID,
                RegisterDate = entity.registerDate,
                TypeID = entity.typeID,
                NextStepID = entity.nextStepID,
                TargetDate = entity.targetDate,
                Description = entity.description,
                OportunityID = entity.oportunityID,           
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}