using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Parameters;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Parameters;
using System.Collections.Generic;

namespace ControleVeiculos.ApplicationService
{
    public class ParameterService : BaseAppService, IParameterService
    {
        private readonly IParameterRepository _parameterRepository;

        public ParameterService(IParameterRepository parameterRepository)
        {
            _parameterRepository = parameterRepository;
        }

        public void Add(MaintenanceParameterCommand command)
        {
            Parameter parameter = new Parameter();

            parameter = parameter.Map(command);

            _parameterRepository.Add(parameter);
        }

        public void Update(MaintenanceParameterCommand command)
        {
            Parameter parameter = new Parameter();

            parameter = parameter.Map(command);

            _parameterRepository.Update(parameter);
        }

        public Result<Parameter> GetByID(int ParameterID)
        {
            var Parameter = _parameterRepository.GetByID(ParameterID);

            return Result.Ok<Parameter>(0, "", Parameter);
        }

        public IPagedList<Parameter> GetAll(FilterParameterCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var parameter = _parameterRepository.GetAll(command);

            return new PagedList<Parameter>(parameter, pageIndex, pageSize);
        }

        public IList<Parameter> GetAll()
        {
            var parameter = _parameterRepository.GetAll();

            return new List<Parameter>(parameter);
        }

        public void Delete(int ParameterID)
        {
            _parameterRepository.Delete(ParameterID);
        }
    }
}

