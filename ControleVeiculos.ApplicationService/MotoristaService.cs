using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Motoristas;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Motoristas;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class MotoristaService : BaseAppService, IMotoristaService
    {
        private readonly IMotoristaRepository _motoristaRepository;

        public MotoristaService(IMotoristaRepository motoristaRepository)
        {
            _motoristaRepository = motoristaRepository;
        }

        public void Add(MaintenanceMotoristaCommand command)
        {
            Motorista motorista = new Motorista();

            motorista = motorista.Map(command);

            _motoristaRepository.Add(motorista);
        }

        public void Update(MaintenanceMotoristaCommand command)
        {
            Motorista motorista = new Motorista();

            motorista = motorista.Map(command);

            _motoristaRepository.Update(motorista);
        }

        //public IList<Motorista> GetAll(int motoristaID)
        //{
        //    var motorista = _motoristaRepository.GetAll(motoristaID);

        //    return new List<Motorista>(motorista);
        //}

        public Result<Motorista> GetByID(int motoristaID)
        {
            var motorista = _motoristaRepository.GetByID(motoristaID);

            return Result.Ok<Motorista>(0, "", motorista);
        }

        public IPagedList<Motorista> GetAll(FilterMotoristaCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var motorista = _motoristaRepository.GetAll(command);

            return new PagedList<Motorista>(motorista, pageIndex, pageSize);
        }

        public void Delete(int motoristaID)
        {
            _motoristaRepository.Delete(motoristaID);
        }
       
    }
}

