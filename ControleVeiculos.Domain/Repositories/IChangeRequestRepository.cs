using ControleVeiculos.Domain.Command.ChangeRequests;
using ControleVeiculos.Domain.Entities.ChangeRequests;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IChangeRequestRepository
    {
        void Add(ChangeRequest changeRequest);
        void Update(ChangeRequest changeRequest);
        ChangeRequest GetByID(int changeRequestID);
        List<ChangeRequest> GetAll(FilterChangeRequestCommand command);
        void Delete(int changeRequestID);
    }
}
