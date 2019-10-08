using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Abastecimentos;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Abastecimentos;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class AbastecimentoService : BaseAppService, IAbastecimentoService
    {
        private readonly IAbastecimentoRepository _abastecimentoRepository;

        public AbastecimentoService(IAbastecimentoRepository abastecimentoRepository)
        {
            _abastecimentoRepository = abastecimentoRepository;
        }

        public void Add(MaintenanceAbastecimentoCommand command)
        {
            Abastecimento abastecimento = new Abastecimento();

            abastecimento = abastecimento.Map(command);

            _abastecimentoRepository.Add(abastecimento);
        }

        public void Update(MaintenanceAbastecimentoCommand command)
        {
            Abastecimento abastecimento = new Abastecimento();

            abastecimento = abastecimento.Map(command);

            _abastecimentoRepository.Update(abastecimento);
        }

        //public IList<Abastecimento> GetAll(int abastecimentoID)
        //{
        //    var abastecimento = _abastecimentoRepository.GetAll(abastecimentoID);

        //    return new List<Abastecimento>(abastecimento);
        //}

        public Result<Abastecimento> GetByID(int abastecimentoID)
        {
            var abastecimento = _abastecimentoRepository.GetByID(abastecimentoID);

            return Result.Ok<Abastecimento>(0, "", abastecimento);
        }

        public IPagedList<Abastecimento> GetAll(FilterAbastecimentoCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var abastecimento = _abastecimentoRepository.GetAll(command);

            return new PagedList<Abastecimento>(abastecimento, pageIndex, pageSize);
        }

        public void Delete(int abastecimentoID)
        {
            _abastecimentoRepository.Delete(abastecimentoID);
        }
       
    }
}

