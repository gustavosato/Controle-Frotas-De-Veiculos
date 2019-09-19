using Lean.Test.Cloud.Domain.Command.Contracts;
using Lean.Test.Cloud.Domain.Entities.Contracts;
using System;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IContractService : IDisposable
    {
        void Add(MaintenanceContractCommand command);
        void Update(MaintenanceContractCommand command);
        Result<Contract> GetByID(int contractID);
        IPagedList<Contract> GetAll(FilterContractCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<Contract> GetAll(int contractID);
        void Delete(int contractID);
    }
}
