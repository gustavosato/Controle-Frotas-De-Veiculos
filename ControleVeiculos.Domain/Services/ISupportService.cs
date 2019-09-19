using Lean.Test.Cloud.Domain.Command.Supports;
using Lean.Test.Cloud.Domain.Entities.Supports;
using System;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface ISupportService : IDisposable
    {
        string Add(MaintenanceSupportCommand command);
        void Update(MaintenanceSupportCommand command);
        Result<Support> GetByID(int defecID);
        IPagedList<Support> GetAll(FilterSupportCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<Support> GetAll(int supportID, int customerID);
        void Delete(int supportD);
    }
}
