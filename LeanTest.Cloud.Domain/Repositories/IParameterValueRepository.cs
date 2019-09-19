using Lean.Test.Cloud.Domain.Command.ParameterValues;
using Lean.Test.Cloud.Domain.Entities.ParameterValues;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IParameterValueRepository
    {
        void Add(ParameterValue parameterValue);
        void Update(ParameterValue parameterValue);
        ParameterValue GetByID(int parameterValueID);
        List<ParameterValue> GetAll(FilterParameterValueCommand command);
        List<ParameterValue> GetAllByParameterID(string parameterID);
        void Delete(int parameterValueID);
        string GetParameterValueByID(int parameterValueID);
    }
}
