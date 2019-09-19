using Lean.Test.Cloud.Domain.Command.EquipmentAccessories;
using System;

namespace Lean.Test.Cloud.Domain.Entities.EquipmentAccessories
{
    public static class EquipmentAccessorieExtensions
    {
        public static Result<EquipmentAccessorie> GetEquipmentAccessorie(this EquipmentAccessorie equipmentAccessorie)
        {
            return Result.Ok(0, "", equipmentAccessorie);
        }

        public static EquipmentAccessorie Map(this EquipmentAccessorie equipmentAccessorie, MaintenanceEquipmentAccessorieCommand command)
        {
            equipmentAccessorie.equipmentAccessorieID = command.EquipmentAccessorieID;
            equipmentAccessorie.description = command.Description;
            equipmentAccessorie.serialNumber = command.SerialNumber;
            equipmentAccessorie.modelName = command.ModelName;
            equipmentAccessorie.assignToID = command.AssignToID;
            equipmentAccessorie.typeID = command.TypeID;
            equipmentAccessorie.invoicing = command.Invoicing;
            equipmentAccessorie.amountInvoicing = command.AmountInvoicing;
            equipmentAccessorie.createdByID = command.CreatedByID;
            equipmentAccessorie.creationDate = command.CreationDate;
            equipmentAccessorie.modifiedByID = command.ModifiedByID;
            equipmentAccessorie.lastModifiedDate = command.LastModifiedDate;
            equipmentAccessorie.startDate = command.StartDate;
            equipmentAccessorie.endDate = command.EndDate;

            return equipmentAccessorie;
        }
    }
}
