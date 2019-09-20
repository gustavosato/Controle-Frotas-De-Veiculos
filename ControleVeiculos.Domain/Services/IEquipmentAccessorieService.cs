using ControleVeiculos.Domain.Command.EquipmentAccessories;
using ControleVeiculos.Domain.Entities.EquipmentAccessories;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IEquipmentAccessorieService : IDisposable
    {
        void Add(MaintenanceEquipmentAccessorieCommand command);
        void Update(MaintenanceEquipmentAccessorieCommand command);
        Result<EquipmentAccessorie> GetByID(int equipmentAccessorieID);
        IPagedList<EquipmentAccessorie> GetAll(FilterEquipmentAccessorieCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int equipmentAccessorieID);
    }
}
