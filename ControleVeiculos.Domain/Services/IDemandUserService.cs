using Lean.Test.Cloud.Domain.Command.DemandsUsers;
using Lean.Test.Cloud.Domain.Entities.DemandsUsers;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IDemandUserService : IDisposable
    {
        void Add(MaintenanceDemandUserCommand command);
        void Delete(int demandID, int usersID);
    }
}
