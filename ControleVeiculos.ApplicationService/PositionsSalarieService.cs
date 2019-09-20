using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.PositionsSalaries;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.PositionsSalaries;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class PositionsSalarieService : BaseAppService, IPositionsSalarieService
    {
        private readonly IPositionsSalarieRepository _positionsSalarieRepository;

        public PositionsSalarieService(IPositionsSalarieRepository positionsSalarieRepository)
        {
            _positionsSalarieRepository = positionsSalarieRepository;
        }

        public void Add(MaintenancePositionsSalarieCommand command)
        {
            PositionsSalarie positionsSalarie = new PositionsSalarie();

            positionsSalarie = positionsSalarie.Map(command);

             _positionsSalarieRepository.Add(positionsSalarie);
        }


        public void Update(MaintenancePositionsSalarieCommand command)
        {
            PositionsSalarie positionsSalarie = new PositionsSalarie();

            positionsSalarie = positionsSalarie.Map(command);

            _positionsSalarieRepository.Update(positionsSalarie);
        }

        public Result<PositionsSalarie> GetByID(int positionsSalarieID)
        {
            var positionsSalarie = _positionsSalarieRepository.GetByID(positionsSalarieID);

            return Result.Ok<PositionsSalarie>(0, "", positionsSalarie);
        }

        public IPagedList<PositionsSalarie> GetAll(FilterPositionsSalarieCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var positionsSalarie = _positionsSalarieRepository.GetAll(command);

            return new PagedList<PositionsSalarie>(positionsSalarie, pageIndex, pageSize);
        }

        public void Delete(int positionsSalarieID)
        {
            _positionsSalarieRepository.Delete(positionsSalarieID);
        }
    }
}

