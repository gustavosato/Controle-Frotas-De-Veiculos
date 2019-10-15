using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Financas;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Financas;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class FinancaService : BaseAppService, IFinancaService
    {
        private readonly IFinancaRepository _financaRepository;

        public FinancaService(IFinancaRepository financaRepository)
        {
            _financaRepository = financaRepository;
        }

        public void Add(MaintenanceFinancaCommand command)
        {
            Financa financa = new Financa();

            financa = financa.Map(command);

            _financaRepository.Add(financa);
        }

        public void Update(MaintenanceFinancaCommand command)
        {
            Financa financa = new Financa();

            financa = financa.Map(command);

            _financaRepository.Update(financa);
        }

        //public IList<Financa> GetAll(int financaID)
        //{
        //    var financa = _financaRepository.GetAll(financaID);

        //    return new List<Financa>(financa);
        //}

        public Result<Financa> GetByID(int financaID)
        {
            var financa = _financaRepository.GetByID(financaID);

            return Result.Ok<Financa>(0, "", financa);
        }

        public IPagedList<Financa> GetAll(FilterFinancaCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var financa = _financaRepository.GetAll(command);

            return new PagedList<Financa>(financa, pageIndex, pageSize);
        }

        public void Delete(int financaID)
        {
            _financaRepository.Delete(financaID);
        }
       
    }
}

