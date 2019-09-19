using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Contracts;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Contracts;
using System.Collections.Generic;

namespace Lean.Test.Cloud.ApplicationService
{
    public class ContractService : BaseAppService, IContractService
    {
        private readonly IContractRepository _contractRepository;

        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public void Add(MaintenanceContractCommand command)
        {
            Contract contract = new Contract();

            contract = contract.Map(command);

            _contractRepository.Add(contract);
        }

        public void Update(MaintenanceContractCommand command)
        {
            Contract contract = new Contract();

            contract = contract.Map(command);

            _contractRepository.Update(contract);
        }

        public Result<Contract> GetByID(int contractID)
        {
            var contract = _contractRepository.GetByID(contractID);

            return Result.Ok<Contract>(0, "", contract);
        }

        public IPagedList<Contract> GetAll(FilterContractCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var contract = _contractRepository.GetAll(command);

            return new PagedList<Contract>(contract, pageIndex, pageSize);
        }

        public IList<Contract> GetAll(int contractID)
        {
            var contract = _contractRepository.GetAll(contractID);

            return new List<Contract>(contract);
        }

        public void Delete(int contractID)
        {
            _contractRepository.Delete(contractID);
        }
    }
}

