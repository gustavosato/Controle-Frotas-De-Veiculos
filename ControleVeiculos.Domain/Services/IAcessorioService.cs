using ControleVeiculos.Domain.Command.Acessorios;
using ControleVeiculos.Domain.Entities.Acessorios;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IAcessorioService : IDisposable
    {
        void Add(MaintenanceAcessorioCommand command);
        void Update(MaintenanceAcessorioCommand command);
        Result<Acessorio> GetByID(int acessorioID);
        IPagedList<Acessorio> GetAll(FilterAcessorioCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int acessorioID);
    }
}
