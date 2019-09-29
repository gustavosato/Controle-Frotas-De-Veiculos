using ControleVeiculos.Domain.Command.Rotas;
using ControleVeiculos.Domain.Entities.Rotas;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IRotaService : IDisposable
    {
        void Add(MaintenanceRotaCommand command);
        void Update(MaintenanceRotaCommand command);
        Result<Rota> GetByID(int rotaID);
        IPagedList<Rota> GetAll(FilterRotaCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int rotaID);
    }
}
