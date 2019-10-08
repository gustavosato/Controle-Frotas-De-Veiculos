using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Acessorios;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Acessorios;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class AcessorioService : BaseAppService, IAcessorioService
    {
        private readonly IAcessorioRepository _acessorioRepository;

        public AcessorioService(IAcessorioRepository acessorioRepository)
        {
            _acessorioRepository = acessorioRepository;
        }

        public void Add(MaintenanceAcessorioCommand command)
        {
            Acessorio acessorio = new Acessorio();

            acessorio = acessorio.Map(command);

            _acessorioRepository.Add(acessorio);
        }

        public void Update(MaintenanceAcessorioCommand command)
        {
            Acessorio acessorio = new Acessorio();

            acessorio = acessorio.Map(command);

            _acessorioRepository.Update(acessorio);
        }

        public IList<Acessorio> GetAll(int acessorioID)
        {
            var acessorio = _acessorioRepository.GetAll(acessorioID);

            return new List<Acessorio>(acessorio);
        }

        public Result<Acessorio> GetByID(int acessorioID)
        {
            var acessorio = _acessorioRepository.GetByID(acessorioID);

            return Result.Ok<Acessorio>(0, "", acessorio);
        }

        public IPagedList<Acessorio> GetAll(FilterAcessorioCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var acessorio = _acessorioRepository.GetAll(command);

            return new PagedList<Acessorio>(acessorio, pageIndex, pageSize);
        }

        public void Delete(int acessorioID)
        {
            _acessorioRepository.Delete(acessorioID);
        }
       
    }
}

