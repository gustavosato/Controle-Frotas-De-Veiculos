using ControleVeiculos.Domain.Command.Abastecimentos;
using ControleVeiculos.Domain.Entities.Abastecimentos;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IAbastecimentoService : IDisposable
    {
        void Add(MaintenanceAbastecimentoCommand command);
        void Update(MaintenanceAbastecimentoCommand command);
        Result<Abastecimento> GetByID(int abastecimentoID);
        IPagedList<Abastecimento> GetAll(FilterAbastecimentoCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int abastecimentoID);
    }
}
