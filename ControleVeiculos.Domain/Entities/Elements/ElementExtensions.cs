using ControleVeiculos.Domain.Command.Elements;
using System;

namespace ControleVeiculos.Domain.Entities.Elements
{
    public static class ElementExtensions
    {
        public static Result<Element> GetElement(this Element element)
        {
            return Result.Ok(0, "", element);
        }

        public static Element Map(this Element element, MaintenanceElementCommand command)
        {

            element.elementID = command.ElementID;
            element.element = command.Element;
            element.actionID = command.ActionID;
            element.defaultValue = command.DefaultValue;
            element.valuePerKilometer = command.ValuePerKilometer;
            element.domains = command.Domains;
            element.automationID = command.AutomationID;
            element.typeIdentificationID = command.TypeIdentificationID;
            element.featureID = command.FeatureID;
            element.createdByID = command.CreatedByID;
            element.creationDate = command.CreationDate;
            element.modifiedByID = command.ModifiedByID;
            element.lastModifiedDate = DateTime.Now.ToString();

            return element;
        }
    }
}
