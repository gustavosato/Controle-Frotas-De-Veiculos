using ControleVeiculos.Domain.Command.Departamentos;
using ControleVeiculos.Domain.Entities.Departamentos;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IDepartamentoService : IDisposable
    {
        void Add(MaintenanceDepartamentoCommand command);
        void Update(MaintenanceDepartamentoCommand command);
        Result<Departamento> GetByID(int departamentoID);
        IPagedList<Departamento> GetAll(FilterDepartamentoCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int departamentoID);
    }
}
