using ControleVeiculos.Domain.Command.SystemParameters;
using ControleVeiculos.Domain.Entities.SystemParameters;
using System;

namespace ControleVeiculos.Domain.Services
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
