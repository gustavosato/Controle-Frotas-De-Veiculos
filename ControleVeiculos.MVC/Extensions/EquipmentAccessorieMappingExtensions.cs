using Lean.Test.Cloud.Domain.Entities.EquipmentAccessories;
using Lean.Test.Cloud.MVC.Models.EquipmentAccessories;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class EquipmentAccessorieMappingExtensions
    {
        public static EquipmentAccessorieModel ToModel(this EquipmentAccessorie entity)
        {
            if (entity == null)
                return null;

            var model = new EquipmentAccessorieModel
            {
                EquipmentAccessorieID = entity.equipmentAccessorieID,
                Description = entity.description,
                SerialNumbers = entity.serialNumber,
                ModelNames = entity.modelName,
                AssignToID = entity.assignToID,
                TypeID = entity.typeID,
                Invoicing = entity.invoicing,
                AmountInvoicing = entity.amountInvoicing,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate,
                StartDate = entity.startDate,
                EndDate = entity.endDate

            };

            return model;
        }
    }
}