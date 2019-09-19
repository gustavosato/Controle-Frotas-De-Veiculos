using Lean.Test.Cloud.Domain.Command.AccountingEntries;
using Lean.Test.Cloud.Domain.Entities.AccountingEntries;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
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
