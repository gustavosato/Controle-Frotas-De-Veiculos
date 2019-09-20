using ControleVeiculos.Domain.Command.ChangeRequests;
using ControleVeiculos.Domain.Entities.ChangeRequests;
using System;

namespace ControleVeiculos.Domain.Services
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
