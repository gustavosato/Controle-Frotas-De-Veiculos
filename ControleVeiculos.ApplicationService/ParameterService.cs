using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Parameters;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Parameters;
using System.Collections.Generic;

namespace Lean.Test.Cloud.ApplicationService
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

