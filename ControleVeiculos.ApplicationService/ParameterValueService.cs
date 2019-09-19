using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.ParameterValues;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.ParameterValues;
using System.Linq;
using System.Collections.Generic;
using Lean.Test.Cloud.SharedKernel.Common;

namespace Lean.Test.Cloud.ApplicationService
{
    public class ParameterValueService : BaseAppService, IParameterValueService
    {
        private readonly IParameterValueRepository _parameterValueRepository;

        public ParameterValueService(IParameterValueRepository parameterValueRepository)
        {
            _parameterValueRepository = parameterValueRepository;
        }

        public void Add(MaintenanceParameterValueCommand command)
        {
            ParameterValue parameterValue = new ParameterValue();

            parameterValue = parameterValue.Map(command);

            _parameterValueRepository.Add(parameterValue);
        }

        public void Update(MaintenanceParameterValueCommand command)
        {
            ParameterValue parameterValue = new ParameterValue();

            parameterValue = parameterValue.Map(command);

            _parameterValueRepository.Update(parameterValue);
        }

        public Result<ParameterValue> GetByID(int parameterValueID)
        {
            var parameterValue = _parameterValueRepository.GetByID(parameterValueID);

            return Result.Ok<ParameterValue>(0, "", parameterValue);
        }

        public IPagedList<ParameterValue> GetAll(FilterParameterValueCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var parameterValue = _parameterValueRepository.GetAll(command);

            return new PagedList<ParameterValue>(parameterValue, pageIndex, pageSize);
        }

        public IList<ParameterValue> GetAllByParameterID(string paramterID)
        {
            var parameterValue = _parameterValueRepository.GetAllByParameterID(paramterID);

            return new List<ParameterValue>(parameterValue);
        }

        public string GetParameterValueByID(int parameterValueID)
        {
            var parameterValue = _parameterValueRepository.GetParameterValueByID(parameterValueID);

            return parameterValue;
        }

        public void Delete(int parameterValueID)
        {
            _parameterValueRepository.Delete(parameterValueID);
        }
    }
}

