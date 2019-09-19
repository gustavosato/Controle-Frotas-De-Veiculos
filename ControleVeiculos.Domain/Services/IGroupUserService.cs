using Lean.Test.Cloud.Domain.Command.Groups;
using Lean.Test.Cloud.Domain.Command.GroupsUsers;
using Lean.Test.Cloud.Domain.Entities.Groups;
using Lean.Test.Cloud.Domain.Entities.GroupsUsers;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IGroupUserService : IDisposable
    {
        void Add(MaintenanceGroupUserCommand command);
        void Delete(int groupID, int usersID);
        IPagedList<Group> GetAllAssociateGroupByUserID(FilterGroupCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<Group> GetAllNoAssociateGroupByUserID(FilterGroupCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
