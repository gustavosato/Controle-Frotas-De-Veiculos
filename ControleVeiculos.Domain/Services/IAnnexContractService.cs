using Lean.Test.Cloud.Domain.Command.AnnexContracts;
using Lean.Test.Cloud.Domain.Entities.AnnexContracts;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IAnnexContractService : IDisposable
    {
        void Add(MaintenanceAnnexContractCommand command);
        void Update(MaintenanceAnnexContractCommand command);
        Result<AnnexContract> GetByID(int annexID);
        IPagedList<AnnexContract> GetAll(FilterAnnexContractCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int annexID);
    }
}
