using Lean.Test.Cloud.Domain.Command.DemandsUsers;
using Lean.Test.Cloud.Domain.Entities.DemandsUsers;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IDemandUserRepository
    {
        void Add(DemandUser DemandUser);
        void Delete(int demandID, int usersID);
    }
}
