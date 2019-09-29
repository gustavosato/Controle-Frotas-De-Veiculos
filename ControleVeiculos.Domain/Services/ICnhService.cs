using ControleVeiculos.Domain.Command.Cnhs;
using ControleVeiculos.Domain.Entities.Cnhs;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface ICnhService : IDisposable
    {
        void Add(MaintenanceCnhCommand command);
        void Update(MaintenanceCnhCommand command);
        Result<Cnh> GetByID(int cnhID);
        IPagedList<Cnh> GetAll(FilterCnhCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int cnhID);
    }
}
