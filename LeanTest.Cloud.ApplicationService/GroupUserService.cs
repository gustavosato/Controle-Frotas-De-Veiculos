using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.GroupsUsers;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.GroupsUsers;
using System.Linq;
using System.Collections.Generic;
using Lean.Test.Cloud.SharedKernel.Common;
using Lean.Test.Cloud.Domain.Entities.Groups;
using Lean.Test.Cloud.Domain.Command.Groups;

namespace Lean.Test.Cloud.ApplicationService
{
    public class GroupUserService : BaseAppService, IGroupUserService
    {
        private readonly IGroupUserRepository _groupUserRepository;

        public GroupUserService(IGroupUserRepository groupUserRepository)
        {
            _groupUserRepository = groupUserRepository;
        }

        public void Add(MaintenanceGroupUserCommand command)
        {
            GroupUser groupUser = new GroupUser();

            groupUser = groupUser.Map(command);

            _groupUserRepository.Add(groupUser);
        }

        public void Delete(int groupID, int userID)
        {
            _groupUserRepository.Delete(groupID, userID);
        }

       public IPagedList<Group> GetAllAssociateGroupByUserID(FilterGroupCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var groups = _groupUserRepository.GetAllAssociateGroupByUserID(command);

            return new PagedList<Group>(groups, pageIndex, pageSize);
        }

        public IPagedList<Group> GetAllNoAssociateGroupByUserID(FilterGroupCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var groups = _groupUserRepository.GetAllNoAssociateGroupByUserID(command);

            return new PagedList<Group>(groups, pageIndex, pageSize);
        }
    }
}

