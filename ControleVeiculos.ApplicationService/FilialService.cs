using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Filiais;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Filiais;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class FilialService : BaseAppService, IFilialService
    {
        private readonly IFilialRepository _filialRepository;

        public FilialService(IFilialRepository filialRepository)
        {
            _filialRepository = filialRepository;
        }

        public void Add(MaintenanceFilialCommand command)
        {
            Filial filial = new Filial();

            filial = filial.Map(command);

            _filialRepository.Add(filial);
        }

        public void Update(MaintenanceFilialCommand command)
        {
            Filial filial = new Filial();

            filial = filial.Map(command);

            _filialRepository.Update(filial);
        }

        //public IList<Filial> GetAll(int filialID)
        //{
        //    var filial = _filialRepository.GetAll(filialID);

        //    return new List<Filial>(filial);
        //}

        public Result<Filial> GetByID(int filialID)
        {
            var filial = _filialRepository.GetByID(filialID);

            return Result.Ok<Filial>(0, "", filial);
        }

        public IPagedList<Filial> GetAll(FilterFilialCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var filial = _filialRepository.GetAll(command);

            return new PagedList<Filial>(filial, pageIndex, pageSize);
        }

        public void Delete(int filialID)
        {
            _filialRepository.Delete(filialID);
        }
       
    }
}

