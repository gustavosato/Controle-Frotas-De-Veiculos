using ControleVeiculos.Domain.Command.AccountingEntries;
using ControleVeiculos.Domain.Entities.AccountingEntries;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IAccountingEntrieService : IDisposable
    {
        string Add(MaintenanceAccountingEntrieCommand command);
        void Update(MaintenanceAccountingEntrieCommand command);
        Result<AccountingEntrie> GetByID(int accountingEntrieID);
        IPagedList<AccountingEntrie> GetAll(FilterAccountingEntrieCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int accountingEntrieID);
    }
}
