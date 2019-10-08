using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Departamentos;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Departamentos;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class DepartamentoService : BaseAppService, IDepartamentoService
    {
        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoService(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        public void Add(MaintenanceDepartamentoCommand command)
        {
            Departamento departamento = new Departamento();

            departamento = departamento.Map(command);

            _departamentoRepository.Add(departamento);
        }

        public void Update(MaintenanceDepartamentoCommand command)
        {
            Departamento departamento = new Departamento();

            departamento = departamento.Map(command);

            _departamentoRepository.Update(departamento);
        }

        public IList<Departamento> GetAll(int departamentoID)
        {
            var departamento = _departamentoRepository.GetAll(departamentoID);

            return new List<Departamento>(departamento);
        }

        public Result<Departamento> GetByID(int departamentoID)
        {
            var departamento = _departamentoRepository.GetByID(departamentoID);

            return Result.Ok<Departamento>(0, "", departamento);
        }

        public IPagedList<Departamento> GetAll(FilterDepartamentoCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var departamento = _departamentoRepository.GetAll(command);

            return new PagedList<Departamento>(departamento, pageIndex, pageSize);
        }

        public void Delete(int departamentoID)
        {
            _departamentoRepository.Delete(departamentoID);
        }
       
    }
}

