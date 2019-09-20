using ControleVeiculos.Domain.Command.ParameterValues;
using ControleVeiculos.Domain.Entities.ParameterValues;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
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
