using Lean.Test.Cloud.Domain.Command.ContractAdditives;
using Lean.Test.Cloud.Domain.Entities.ContractAdditives;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IContractAdditiveRepository
    {
        void Add(ContractAdditive contractAdditive);
        void Update(ContractAdditive contractAdditive);
        ContractAdditive GetByID(int additiveID);
        List<ContractAdditive> GetAll(FilterContractAdditiveCommand command);
        void Delete(int additiveID);
    }
}
