using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Manutencoes;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Manutencoes;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class ManutencaoService : BaseAppService, IManutencaoService
    {
        private readonly IManutencaoRepository _manutencaoRepository;

        public ManutencaoService(IManutencaoRepository manutencaoRepository)
        {
            _manutencaoRepository = manutencaoRepository;
        }

        public void Add(MaintenanceManutencaoCommand command)
        {
            Manutencao manutencao = new Manutencao();

            manutencao = manutencao.Map(command);

            _manutencaoRepository.Add(manutencao);
        }

        public void Update(MaintenanceManutencaoCommand command)
        {
            Manutencao manutencao = new Manutencao();

            manutencao = manutencao.Map(command);

            _manutencaoRepository.Update(manutencao);
        }

        //public IList<Manutencao> GetAll(int manutencaoID)
        //{
        //    var manutencao = _manutencaoRepository.GetAll(manutencaoID);

        //    return new List<Manutencao>(manutencao);
        //}

        public Result<Manutencao> GetByID(int manutencaoID)
        {
            var manutencao = _manutencaoRepository.GetByID(manutencaoID);

            return Result.Ok<Manutencao>(0, "", manutencao);
        }

        public IPagedList<Manutencao> GetAll(FilterManutencaoCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var manutencao = _manutencaoRepository.GetAll(command);

            return new PagedList<Manutencao>(manutencao, pageIndex, pageSize);
        }

        public void Delete(int manutencaoID)
        {
            _manutencaoRepository.Delete(manutencaoID);
        }
       
    }
}

