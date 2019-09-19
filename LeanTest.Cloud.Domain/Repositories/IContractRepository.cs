using Lean.Test.Cloud.Domain.Command.Contracts;
using Lean.Test.Cloud.Domain.Entities.Contracts;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IContractRepository
    {
        void Add(Contract contract);
        void Update(Contract contract);
        Contract GetByID(int contractID);
        List<Contract> GetAll(FilterContractCommand command);
        List<Contract> GetAll(int contractID);
        void Delete(int contractID);
    }
}
