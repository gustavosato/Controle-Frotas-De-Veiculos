using ControleVeiculos.Domain.Command.Multas;
using ControleVeiculos.Domain.Entities.Multas;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IMultaService : IDisposable
    {
        void Add(MaintenanceMultaCommand command);
        void Update(MaintenanceMultaCommand command);
        Result<Multa> GetByID(int multaID);
        IPagedList<Multa> GetAll(FilterMultaCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int multaID);
    }
}
