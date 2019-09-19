using Lean.Test.Cloud.Domain.Command.Groups;
using Lean.Test.Cloud.Domain.Command.GroupsUsers;
using Lean.Test.Cloud.Domain.Entities.Groups;
using Lean.Test.Cloud.Domain.Entities.GroupsUsers;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IGroupUserRepository
    {
        void Add(GroupUser GroupUser);
        void Delete(int groupID, int usersID);
        List<Group> GetAllAssociateGroupByUserID(FilterGroupCommand command);
        List<Group> GetAllNoAssociateGroupByUserID(FilterGroupCommand command);
    }
}
