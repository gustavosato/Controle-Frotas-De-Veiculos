using Lean.Test.Cloud.Domain.Command.ParameterValues;
using Lean.Test.Cloud.Domain.Entities.ParameterValues;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IParameterValueService : IDisposable
    {
        void Add(MaintenanceParameterValueCommand parameterValue);
        void Update(MaintenanceParameterValueCommand parameterValue);
        Result <ParameterValue> GetByID(int parameterValueID);
        IPagedList<ParameterValue> GetAll(FilterParameterValueCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<ParameterValue> GetAllByParameterID(string parameterID);
        void Delete(int userID);
        string GetParameterValueByID(int parameterValueID);
    }
}
