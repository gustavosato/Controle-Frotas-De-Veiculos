using Lean.Test.Cloud.Domain.Command.Parameters;
using Lean.Test.Cloud.Domain.Entities.Parameters;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IParameterRepository
    {
        void Add(Parameter parameter);
        void Update(Parameter parameter);
        Parameter GetByID(int parameterID);
        List<Parameter> GetAll(FilterParameterCommand command);
        List<Parameter> GetAll();
        void Delete(int parameterID);
    }
}
