using Lean.Test.Cloud.Domain.Command.AnnexContracts;
using Lean.Test.Cloud.Domain.Entities.AnnexContracts;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IAnnexContractRepository
    {
        void Add(AnnexContract annexContract);
        void Update(AnnexContract annexContract);
        AnnexContract GetByID(int annexID);
        List<AnnexContract> GetAll(FilterAnnexContractCommand command);
        void Delete(int annexID);
    }
}
