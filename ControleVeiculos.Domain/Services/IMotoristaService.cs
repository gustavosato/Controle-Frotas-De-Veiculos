using ControleVeiculos.Domain.Command.Motoristas;
using ControleVeiculos.Domain.Entities.Motoristas;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IMotoristaService : IDisposable
    {
        void Add(MaintenanceMotoristaCommand command);
        void Update(MaintenanceMotoristaCommand command);
        Result<Motorista> GetByID(int motoristaID);
        IPagedList<Motorista> GetAll(FilterMotoristaCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int motoristaID);
    }
}
