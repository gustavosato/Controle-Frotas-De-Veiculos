using ControleVeiculos.Domain.Command.Sinistros;
using ControleVeiculos.Domain.Entities.Sinistros;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface ISinistroService : IDisposable
    {
        void Add(MaintenanceSinistroCommand command);
        void Update(MaintenanceSinistroCommand command);
        Result<Sinistro> GetByID(int sinistroID);
        IPagedList<Sinistro> GetAll(FilterSinistroCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int sinistroID);
    }
}
