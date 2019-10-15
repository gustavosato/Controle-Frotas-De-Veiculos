using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Sinistros;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Sinistros;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class SinistroService : BaseAppService, ISinistroService
    {
        private readonly ISinistroRepository _sinistroRepository;

        public SinistroService(ISinistroRepository sinistroRepository)
        {
            _sinistroRepository = sinistroRepository;
        }

        public void Add(MaintenanceSinistroCommand command)
        {
            Sinistro sinistro = new Sinistro();

            sinistro = sinistro.Map(command);

            _sinistroRepository.Add(sinistro);
        }

        public void Update(MaintenanceSinistroCommand command)
        {
            Sinistro sinistro = new Sinistro();

            sinistro = sinistro.Map(command);

            _sinistroRepository.Update(sinistro);
        }

        //public IList<Sinistro> GetAll(int sinistroID)
        //{
        //    var sinistro = _sinistroRepository.GetAll(sinistroID);

        //    return new List<Sinistro>(sinistro);
        //}

        public Result<Sinistro> GetByID(int sinistroID)
        {
            var sinistro = _sinistroRepository.GetByID(sinistroID);

            return Result.Ok<Sinistro>(0, "", sinistro);
        }

        public IPagedList<Sinistro> GetAll(FilterSinistroCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var sinistro = _sinistroRepository.GetAll(command);

            return new PagedList<Sinistro>(sinistro, pageIndex, pageSize);
        }

        public void Delete(int sinistroID)
        {
            _sinistroRepository.Delete(sinistroID);
        }
       
    }
}

