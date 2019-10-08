using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Seguros;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Seguros;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class SeguroService : BaseAppService, ISeguroService
    {
        private readonly ISeguroRepository _seguroRepository;

        public SeguroService(ISeguroRepository seguroRepository)
        {
            _seguroRepository = seguroRepository;
        }

        public void Add(MaintenanceSeguroCommand command)
        {
            Seguro seguro = new Seguro();

            seguro = seguro.Map(command);

            _seguroRepository.Add(seguro);
        }

        public void Update(MaintenanceSeguroCommand command)
        {
            Seguro seguro = new Seguro();

            seguro = seguro.Map(command);

            _seguroRepository.Update(seguro);
        }

        public IList<Seguro> GetAll(int seguroID)
        {
            var seguro = _seguroRepository.GetAll(seguroID);

            return new List<Seguro>(seguro);
        }

        public Result<Seguro> GetByID(int seguroID)
        {
            var seguro = _seguroRepository.GetByID(seguroID);

            return Result.Ok<Seguro>(0, "", seguro);
        }

        public IPagedList<Seguro> GetAll(FilterSeguroCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var seguro = _seguroRepository.GetAll(command);

            return new PagedList<Seguro>(seguro, pageIndex, pageSize);
        }

        public void Delete(int seguroID)
        {
            _seguroRepository.Delete(seguroID);
        }
       
    }
}

