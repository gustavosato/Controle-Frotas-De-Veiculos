using ControleVeiculos.Domain.Command.Parameters;
using ControleVeiculos.Domain.Entities.Parameters;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
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
