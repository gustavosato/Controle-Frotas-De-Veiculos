using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.EntradaSaidas;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.EntradaSaidas;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class EntradaSaidaService : BaseAppService, IEntradaSaidaService
    {
        private readonly IEntradaSaidaRepository _entradaSaidaRepository;

        public EntradaSaidaService(IEntradaSaidaRepository entradaSaidaRepository)
        {
            _entradaSaidaRepository = entradaSaidaRepository;
        }

        public void Add(MaintenanceEntradaSaidaCommand command)
        {
            EntradaSaida entradaSaida = new EntradaSaida();

            entradaSaida = entradaSaida.Map(command);

            _entradaSaidaRepository.Add(entradaSaida);
        }

        public void Update(MaintenanceEntradaSaidaCommand command)
        {
            EntradaSaida entradaSaida = new EntradaSaida();

            entradaSaida = entradaSaida.Map(command);

            _entradaSaidaRepository.Update(entradaSaida);
        }

        public IList<EntradaSaida> GetAll(int entradaSaidaID)
        {
            var entradaSaida = _entradaSaidaRepository.GetAll(entradaSaidaID);

            return new List<EntradaSaida>(entradaSaida);
        }

        public Result<EntradaSaida> GetByID(int entradaSaidaID)
        {
            var entradaSaida = _entradaSaidaRepository.GetByID(entradaSaidaID);

            return Result.Ok<EntradaSaida>(0, "", entradaSaida);
        }

        public IPagedList<EntradaSaida> GetAll(FilterEntradaSaidaCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var entradaSaida = _entradaSaidaRepository.GetAll(command);

            return new PagedList<EntradaSaida>(entradaSaida, pageIndex, pageSize);
        }

        public void Delete(int entradaSaidaID)
        {
            _entradaSaidaRepository.Delete(entradaSaidaID);
        }
       
    }
}

