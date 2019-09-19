using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.ContractAdditives;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.ContractAdditives;
using System.Collections.Generic;

namespace Lean.Test.Cloud.ApplicationService
{
    public class ContractAdditiveService : BaseAppService, IContractAdditiveService
    {
        private readonly IContractAdditiveRepository _contractAdditiveRepository;

        public ContractAdditiveService(IContractAdditiveRepository contractAdditiveRepository)
        {
            _contractAdditiveRepository = contractAdditiveRepository;
        }

        public void Add(MaintenanceContractAdditiveCommand command)
        {
            ContractAdditive contractAdditive = new ContractAdditive();

            contractAdditive = contractAdditive.Map(command);

            _contractAdditiveRepository.Add(contractAdditive);
        }

        public void Update(MaintenanceContractAdditiveCommand command)
        {
            ContractAdditive contractAdditive = new ContractAdditive();

            contractAdditive = contractAdditive.Map(command);

            _contractAdditiveRepository.Update(contractAdditive);
        }

        public Result<ContractAdditive> GetByID(int contractAdditiveID)
        {
            var contractAdditive = _contractAdditiveRepository.GetByID(contractAdditiveID);

            return Result.Ok<ContractAdditive>(0, "", contractAdditive);
        }

        public IPagedList<ContractAdditive> GetAll(FilterContractAdditiveCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var contractAdditive = _contractAdditiveRepository.GetAll(command);

            return new PagedList<ContractAdditive>(contractAdditive, pageIndex, pageSize);
        }

        public void Delete(int contractAdditiveID)
        {
            _contractAdditiveRepository.Delete(contractAdditiveID);
        }
    }
}

