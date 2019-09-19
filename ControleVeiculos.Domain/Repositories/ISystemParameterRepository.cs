using Lean.Test.Cloud.Domain.Command.SystemParameters;
using Lean.Test.Cloud.Domain.Entities.SystemParameters;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface ISystemParameterRepository
    {
        void Add(SystemParameter systemParameter);
        void Update(SystemParameter systemParameter);
        SystemParameter GetByID(int parameterID);
        List<SystemParameter> GetAll(FilterSystemParameterCommand command);
        List<SystemParameter> GetAll();
        void Delete(int parameterID);
    }
}
