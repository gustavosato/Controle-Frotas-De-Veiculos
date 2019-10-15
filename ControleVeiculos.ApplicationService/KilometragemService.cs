using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Kilometragens;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Kilometragens;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class KilometragemService : BaseAppService, IKilometragemService
    {
        private readonly IKilometragemRepository _kilometragemRepository;

        public KilometragemService(IKilometragemRepository kilometragemRepository)
        {
            _kilometragemRepository = kilometragemRepository;
        }

        public void Add(MaintenanceKilometragemCommand command)
        {
            Kilometragem kilometragem = new Kilometragem();

            kilometragem = kilometragem.Map(command);

            _kilometragemRepository.Add(kilometragem);
        }

        public void Update(MaintenanceKilometragemCommand command)
        {
            Kilometragem kilometragem = new Kilometragem();

            kilometragem = kilometragem.Map(command);

            _kilometragemRepository.Update(kilometragem);
        }

        //public IList<Kilometragem> GetAll(int kilometragemID)
        //{
        //    var kilometragem = _kilometragemRepository.GetAll(kilometragemID);

        //    return new List<Kilometragem>(kilometragem);
        //}

        public Result<Kilometragem> GetByID(int kilometragemID)
        {
            var kilometragem = _kilometragemRepository.GetByID(kilometragemID);

            return Result.Ok<Kilometragem>(0, "", kilometragem);
        }

        public IPagedList<Kilometragem> GetAll(FilterKilometragemCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var kilometragem = _kilometragemRepository.GetAll(command);

            return new PagedList<Kilometragem>(kilometragem, pageIndex, pageSize);
        }

        public void Delete(int kilometragemID)
        {
            _kilometragemRepository.Delete(kilometragemID);
        }
       
    }
}

