using ControleVeiculos.Domain.Command.Manutencaos;
using ControleVeiculos.Domain.Entities.Manutencaos;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IManutencaoService : IDisposable
    {
        void Add(MaintenanceManutencaoCommand command);
        void Update(MaintenanceManutencaoCommand command);
        Result<Manutencao> GetByID(int manutencaoID);
        IPagedList<Manutencao> GetAll(FilterManutencaoCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int manutencaoID);
    }
}
