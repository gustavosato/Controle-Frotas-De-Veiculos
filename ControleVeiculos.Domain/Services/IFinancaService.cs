using ControleVeiculos.Domain.Command.Financas;
using ControleVeiculos.Domain.Entities.Financas;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IFinancaService : IDisposable
    {
        void Add(MaintenanceFinancaCommand command);
        void Update(MaintenanceFinancaCommand command);
        Result<Financa> GetByID(int financaID);
        IPagedList<Financa> GetAll(FilterFinancaCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int financaID);
    }
}
