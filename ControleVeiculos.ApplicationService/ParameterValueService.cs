using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.ParameterValues;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.ParameterValues;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
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

