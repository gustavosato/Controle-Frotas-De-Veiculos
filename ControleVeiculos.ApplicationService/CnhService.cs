using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Cnhs;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Cnhs;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class CnhService : BaseAppService, ICnhService
    {
        private readonly ICnhRepository _cnhRepository;

        public CnhService(ICnhRepository cnhRepository)
        {
            _cnhRepository = cnhRepository;
        }

        public void Add(MaintenanceCnhCommand command)
        {
            Cnh cnh = new Cnh();

            cnh = cnh.Map(command);

            _cnhRepository.Add(cnh);
        }

        public void Update(MaintenanceCnhCommand command)
        {
            Cnh cnh = new Cnh();

            cnh = cnh.Map(command);

            _cnhRepository.Update(cnh);
        }

        //public IList<Cnh> GetAll(int cnhID)
        //{
        //    var cnh = _cnhRepository.GetAll(cnhID);

        //    return new List<Cnh>(cnh);
        //}

        public Result<Cnh> GetByID(int cnhID)
        {
            var cnh = _cnhRepository.GetByID(cnhID);

            return Result.Ok<Cnh>(0, "", cnh);
        }

        public IPagedList<Cnh> GetAll(FilterCnhCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var cnh = _cnhRepository.GetAll(command);

            return new PagedList<Cnh>(cnh, pageIndex, pageSize);
        }

        public void Delete(int cnhID)
        {
            _cnhRepository.Delete(cnhID);
        }
       
    }
}

