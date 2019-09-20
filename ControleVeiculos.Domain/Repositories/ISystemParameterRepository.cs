using ControleVeiculos.Domain.Command.SystemParameters;
using ControleVeiculos.Domain.Entities.SystemParameters;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
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
