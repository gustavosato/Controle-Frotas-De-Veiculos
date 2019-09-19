using Lean.Test.Cloud.Domain.Command.ChangeRequests;
using Lean.Test.Cloud.Domain.Entities.ChangeRequests;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IChangeRequestService : IDisposable
    {
        void Add(MaintenanceChangeRequestCommand command);
        void Update(MaintenanceChangeRequestCommand command);
        Result<ChangeRequest> GetByID(int changeRequestID);
        IPagedList<ChangeRequest> GetAll(FilterChangeRequestCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int changeRequestID);
    }
}
