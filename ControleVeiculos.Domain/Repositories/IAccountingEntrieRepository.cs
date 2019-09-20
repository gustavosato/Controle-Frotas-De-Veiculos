using ControleVeiculos.Domain.Command.AccountingEntries;
using ControleVeiculos.Domain.Entities.AccountingEntries;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IAccountingEntrieRepository
    {
        string Add(AccountingEntrie accountingEntrie);
        void Update(AccountingEntrie accountingEntrie);
        AccountingEntrie GetByID(int accountingEntrieID);
        List<AccountingEntrie> GetAll(FilterAccountingEntrieCommand command);
        void Delete(int accountingEntrieID);
    }
}
