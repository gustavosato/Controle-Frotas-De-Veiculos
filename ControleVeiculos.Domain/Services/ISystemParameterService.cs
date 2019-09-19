using Lean.Test.Cloud.Domain.Command.SystemParameters;
using Lean.Test.Cloud.Domain.Entities.SystemParameters;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface ISystemParameterService : IDisposable
    {
        void Add(MaintenanceSystemParameterCommand command);
        void Update(MaintenanceSystemParameterCommand command);
        Result<SystemParameter> GetByID(int parameterID);
        IPagedList<SystemParameter> GetAll(FilterSystemParameterCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int parameterID);
    }
}
