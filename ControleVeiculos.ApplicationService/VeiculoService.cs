using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Veiculos;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Veiculos;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class VeiculoService : BaseAppService, IVeiculoService
    {
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoService(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        public void Add(MaintenanceVeiculoCommand command)
        {
            Veiculo veiculo = new Veiculo();

            veiculo = veiculo.Map(command);

            _veiculoRepository.Add(veiculo);
        }

        public void Update(MaintenanceVeiculoCommand command)
        {
            Veiculo veiculo = new Veiculo();

            veiculo = veiculo.Map(command);

            _veiculoRepository.Update(veiculo);
        }

        public IList<Veiculo> GetAll(int veiculoID)
        {
            var veiculo = _veiculoRepository.GetAll(veiculoID);

            return new List<Veiculo>(veiculo);
        }

        public Result<Veiculo> GetByID(int veiculoID)
        {
            var veiculo = _veiculoRepository.GetByID(veiculoID);

            return Result.Ok<Veiculo>(0, "", veiculo);
        }

        public IPagedList<Veiculo> GetAll(FilterVeiculoCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var veiculo = _veiculoRepository.GetAll(command);

            return new PagedList<Veiculo>(veiculo, pageIndex, pageSize);
        }

        public void Delete(int veiculoID)
        {
            _veiculoRepository.Delete(veiculoID);
        }
       
    }
}

