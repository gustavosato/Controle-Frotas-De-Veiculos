using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Rotas;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Rotas;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class RotaService : BaseAppService, IRotaService
    {
        private readonly IRotaRepository _rotaRepository;

        public RotaService(IRotaRepository rotaRepository)
        {
            _rotaRepository = rotaRepository;
        }

        public void Add(MaintenanceRotaCommand command)
        {
            Rota rota = new Rota();

            rota = rota.Map(command);

            _rotaRepository.Add(rota);
        }

        public void Update(MaintenanceRotaCommand command)
        {
            Rota rota = new Rota();

            rota = rota.Map(command);

            _rotaRepository.Update(rota);
        }

        //public IList<Rota> GetAll(int rotaID)
        //{
        //    var rota = _rotaRepository.GetAll(rotaID);

        //    return new List<Rota>(rota);
        //}

        public Result<Rota> GetByID(int rotaID)
        {
            var rota = _rotaRepository.GetByID(rotaID);

            return Result.Ok<Rota>(0, "", rota);
        }

        public IPagedList<Rota> GetAll(FilterRotaCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var rota = _rotaRepository.GetAll(command);

            return new PagedList<Rota>(rota, pageIndex, pageSize);
        }

        public void Delete(int rotaID)
        {
            _rotaRepository.Delete(rotaID);
        }
       
    }
}

