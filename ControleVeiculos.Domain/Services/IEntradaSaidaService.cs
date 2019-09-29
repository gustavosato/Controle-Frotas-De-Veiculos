using ControleVeiculos.Domain.Command.EntradaSaidas;
using ControleVeiculos.Domain.Entities.EntradaSaidas;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IEntradaSaidaService : IDisposable
    {
        void Add(MaintenanceEntradaSaidaCommand command);
        void Update(MaintenanceEntradaSaidaCommand command);
        Result<EntradaSaida> GetByID(int entradaSaidaID);
        IPagedList<EntradaSaida> GetAll(FilterEntradaSaidaCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int entradaSaidaID);
    }
}
