using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.AnnexContracts;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.AnnexContracts;
using System.Collections.Generic;

namespace ControleVeiculos.ApplicationService
{
    public class AnnexContractService : BaseAppService, IAnnexContractService
    {
        private readonly IAnnexContractRepository _annexContractRepository;

        public AnnexContractService(IAnnexContractRepository annexContractRepository)
        {
            _annexContractRepository = annexContractRepository;
        }

        public void Add(MaintenanceAnnexContractCommand command)
        {
            AnnexContract annexContract = new AnnexContract();

            annexContract = annexContract.Map(command);

            _annexContractRepository.Add(annexContract);
        }

        public void Update(MaintenanceAnnexContractCommand command)
        {
            AnnexContract annexContract = new AnnexContract();

            annexContract = annexContract.Map(command);

            _annexContractRepository.Update(annexContract);
        }

        public Result<AnnexContract> GetByID(int annexContractID)
        {
            var annexContract = _annexContractRepository.GetByID(annexContractID);

            return Result.Ok<AnnexContract>(0, "", annexContract);
        }

        public IPagedList<AnnexContract> GetAll(FilterAnnexContractCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var annexContract = _annexContractRepository.GetAll(command);

            return new PagedList<AnnexContract>(annexContract, pageIndex, pageSize);
        }

        public void Delete(int annexContractID)
        {
            _annexContractRepository.Delete(annexContractID);
        }
    }
}

