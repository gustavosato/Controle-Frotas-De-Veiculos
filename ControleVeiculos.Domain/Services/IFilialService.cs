using ControleVeiculos.Domain.Command.Filiais;
using ControleVeiculos.Domain.Entities.Filiais;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IFilialService : IDisposable
    {
        void Add(MaintenanceFilialCommand command);
        void Update(MaintenanceFilialCommand command);
        Result<Filial> GetByID(int filialID);
        IPagedList<Filial> GetAll(FilterFilialCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int filialID);
    }
}
