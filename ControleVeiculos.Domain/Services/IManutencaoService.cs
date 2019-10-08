using ControleVeiculos.Domain.Command.Manutencoes;
using ControleVeiculos.Domain.Entities.Manutencoes;
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
