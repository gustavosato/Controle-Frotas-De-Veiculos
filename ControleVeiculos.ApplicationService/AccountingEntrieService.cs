using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.AccountingEntries;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.AccountingEntries;

namespace ControleVeiculos.ApplicationService
{
    public class AccountingEntrieService : BaseAppService, IAccountingEntrieService
    {
        private readonly IAccountingEntrieRepository _accountingEntrieRepository;

        public AccountingEntrieService(IAccountingEntrieRepository accountingEntrieRepository)
        {
            _accountingEntrieRepository = accountingEntrieRepository;
        }


        public string Add(MaintenanceAccountingEntrieCommand command)
        {
            AccountingEntrie accountingEntrie = new AccountingEntrie();

            accountingEntrie = accountingEntrie.Map(command);
            
            return _accountingEntrieRepository.Add(accountingEntrie);
        }

        public void Update(MaintenanceAccountingEntrieCommand command)
        {
            AccountingEntrie accountingEntrie = new AccountingEntrie();

            accountingEntrie = accountingEntrie.Map(command);

            _accountingEntrieRepository.Update(accountingEntrie);
        }

        public Result<AccountingEntrie> GetByID(int accountingEntrieID)
        {
            var accountingEntrie = _accountingEntrieRepository.GetByID(accountingEntrieID);

            return Result.Ok<AccountingEntrie>(0, "", accountingEntrie);
        }

        public IPagedList<AccountingEntrie> GetAll(FilterAccountingEntrieCommand filterAccountingEntrieCommand, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var accountingEntrie = _accountingEntrieRepository.GetAll(filterAccountingEntrieCommand);

            return new PagedList<AccountingEntrie>(accountingEntrie, pageIndex, pageSize);
        }

        public void Delete(int accountingEntrieID)
        {
            _accountingEntrieRepository.Delete(accountingEntrieID);
        }
    }
}

