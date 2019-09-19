using Lean.Test.Cloud.Domain.Command.ContractAdditives;
using Lean.Test.Cloud.Domain.Entities.ContractAdditives;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IContractAdditiveService : IDisposable
    {
        void Add(MaintenanceContractAdditiveCommand command);
        void Update(MaintenanceContractAdditiveCommand command);
        Result<ContractAdditive> GetByID(int additiveID);
        IPagedList<ContractAdditive> GetAll(FilterContractAdditiveCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int additiveID);
    }
}
