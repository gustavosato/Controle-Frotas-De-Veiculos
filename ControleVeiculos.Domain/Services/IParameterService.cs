using Lean.Test.Cloud.Domain.Command.Parameters;
using Lean.Test.Cloud.Domain.Entities.Parameters;
using System;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IParameterService : IDisposable
    {
        void Add(MaintenanceParameterCommand command);
        void Update(MaintenanceParameterCommand command);
        Result<Parameter> GetByID(int parameterID);
        IPagedList<Parameter> GetAll(FilterParameterCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<Parameter> GetAll();
        void Delete(int parameterID);
    }
}
