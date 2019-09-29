using ControleVeiculos.Domain.Command.Departamentos;
using ControleVeiculos.Domain.Entities.Departamentos;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IDepartamentoRepository
    {
        void Add(Departamento departamento);
        void Update(Departamento departamento);
        Departamento GetByID(int departamentoID);
        List<Departamento> GetAll(FilterDepartamentoCommand command);
        void Delete(int departamentoID);
    }
}
