using ControleVeiculos.Domain.Entities.Elements;
using ControleVeiculos.MVC.Models.Elements;

namespace ControleVeiculos.MVC.Extensions
{
    public static class ElementMappingExtensions
    {
        public static ElementModel ToModel(this Element entity)
        {
            if (entity == null)
                return null;

            var model = new ElementModel
            {
                ElementID = entity.elementID,
                Element = entity.element,
                ActionID = entity.actionID,
                DefaultValue = entity.defaultValue,
                Domains = entity.domains,
                AutomationID = entity.automationID,
                TypeIdentificationID = entity.typeIdentificationID,
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