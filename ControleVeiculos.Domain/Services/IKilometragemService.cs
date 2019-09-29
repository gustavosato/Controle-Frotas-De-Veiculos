using ControleVeiculos.Domain.Command.Kilometragems;
using ControleVeiculos.Domain.Entities.Kilometragems;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IKilometragemService : IDisposable
    {
        void Add(MaintenanceKilometragemCommand command);
        void Update(MaintenanceKilometragemCommand command);
        Result<Kilometragem> GetByID(int kilometragemID);
        IPagedList<Kilometragem> GetAll(FilterKilometragemCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int kilometragemID);
    }
}
