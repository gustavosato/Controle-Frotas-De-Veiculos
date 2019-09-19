using Lean.Test.Cloud.Domain.Command.ChangeRequests;
using Lean.Test.Cloud.Domain.Entities.ChangeRequests;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
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
