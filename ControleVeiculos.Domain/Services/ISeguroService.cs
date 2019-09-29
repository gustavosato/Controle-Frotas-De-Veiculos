using ControleVeiculos.Domain.Command.Seguros;
using ControleVeiculos.Domain.Entities.Seguros;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface ISeguroService : IDisposable
    {
        void Add(MaintenanceSeguroCommand command);
        void Update(MaintenanceSeguroCommand command);
        Result<Seguro> GetByID(int seguroID);
        IPagedList<Seguro> GetAll(FilterSeguroCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int seguroID);
    }
}
