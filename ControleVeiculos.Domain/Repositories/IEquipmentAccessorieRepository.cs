using ControleVeiculos.Domain.Command.EquipmentAccessories;
using ControleVeiculos.Domain.Entities.EquipmentAccessories;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
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
