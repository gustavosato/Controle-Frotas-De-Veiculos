using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.EquipmentAccessories;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.EquipmentAccessories;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class EquipmentAccessorieService : BaseAppService, IEquipmentAccessorieService
    {
        private readonly IEquipmentAccessorieRepository _equipmentAccessorieRepository;

        public EquipmentAccessorieService(IEquipmentAccessorieRepository equipmentAccessorieRepository)
        {
            _equipmentAccessorieRepository = equipmentAccessorieRepository;
        }

        public void Add(MaintenanceEquipmentAccessorieCommand command)
        {
            EquipmentAccessorie equipmentAccessorie = new EquipmentAccessorie();

            equipmentAccessorie = equipmentAccessorie.Map(command);

            _equipmentAccessorieRepository.Add(equipmentAccessorie);
        }

        public void Update(MaintenanceEquipmentAccessorieCommand command)
        {
            EquipmentAccessorie equipmentAccessorie = new EquipmentAccessorie();

            equipmentAccessorie = equipmentAccessorie.Map(command);

            _equipmentAccessorieRepository.Update(equipmentAccessorie);
        }

        public Result<EquipmentAccessorie> GetByID(int equipmentAccessorieID)
        {
            var equipmentAccessorie = _equipmentAccessorieRepository.GetByID(equipmentAccessorieID);

            return Result.Ok<EquipmentAccessorie>(0, "", equipmentAccessorie);
        }

        public IPagedList<EquipmentAccessorie> GetAll(FilterEquipmentAccessorieCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var equipmentAccessorie = _equipmentAccessorieRepository.GetAll(command);

            return new PagedList<EquipmentAccessorie>(equipmentAccessorie, pageIndex, pageSize);
        }

        public void Delete(int equipmentAccessorieID)
        {
            _equipmentAccessorieRepository.Delete(equipmentAccessorieID);
        }
    }
}

