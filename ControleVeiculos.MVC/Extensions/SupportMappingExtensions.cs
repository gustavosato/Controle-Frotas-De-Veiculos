using ControleVeiculos.Domain.Entities.Supports;
using ControleVeiculos.MVC.Models.Supports;

namespace ControleVeiculos.MVC.Extensions
{
    public static class SupportMappingExtensions
    {
        public static SupportModel ToModel(this Support entity)
        {
            if (entity == null)
                return null;

            var model = new SupportModel
            {
                SupportID = entity.supportID,
                Summary = entity.summary,
                Description = entity.description,
                SeverityID = entity.severityID,
                StatusID = entity.statusID,
                PriorityID = entity.priorityID,
                TypeID = entity.typeID,
                AssingToID = entity.assingToID,
                ResolutionDate = entity.resolutionDate,
                CustomerID = entity.customerID,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}