using ControleVeiculos.Domain.Entities.PipelineEvents;
using ControleVeiculos.MVC.Models.PipelineEvents;

namespace ControleVeiculos.MVC.Extensions
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