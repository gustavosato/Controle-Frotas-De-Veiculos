using Lean.Test.Cloud.Domain.Command.EquipmentAccessories;
using Lean.Test.Cloud.Domain.Entities.EquipmentAccessories;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IEquipmentAccessorieRepository
    {
        void Add(EquipmentAccessorie equipmentAccessorie);
        void Update(EquipmentAccessorie equipmentAccessorie);
        EquipmentAccessorie GetByID(int equipmentAccessorieID);
        List<EquipmentAccessorie> GetAll(FilterEquipmentAccessorieCommand command);
        void Delete(int equipmentAccessorieID);
    }
}
