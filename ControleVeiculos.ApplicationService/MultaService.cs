using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Multas;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Multas;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class MultaService : BaseAppService, IMultaService
    {
        private readonly IMultaRepository _multaRepository;

        public MultaService(IMultaRepository multaRepository)
        {
            _multaRepository = multaRepository;
        }

        public void Add(MaintenanceMultaCommand command)
        {
            Multa multa = new Multa();

            multa = multa.Map(command);

            _multaRepository.Add(multa);
        }

        public void Update(MaintenanceMultaCommand command)
        {
            Multa multa = new Multa();

            multa = multa.Map(command);

            _multaRepository.Update(multa);
        }

        public IList<Multa> GetAll(int multaID)
        {
            var multa = _multaRepository.GetAll(multaID);

            return new List<Multa>(multa);
        }

        public Result<Multa> GetByID(int multaID)
        {
            var multa = _multaRepository.GetByID(multaID);

            return Result.Ok<Multa>(0, "", multa);
        }

        public IPagedList<Multa> GetAll(FilterMultaCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var multa = _multaRepository.GetAll(command);

            return new PagedList<Multa>(multa, pageIndex, pageSize);
        }

        public void Delete(int multaID)
        {
            _multaRepository.Delete(multaID);
        }
       
    }
}

