using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Groups;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Groups;
using System.Collections.Generic;

namespace Lean.Test.Cloud.ApplicationService
{
    public class GroupService : BaseAppService, IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public void Add(MaintenanceGroupCommand command)
        {
            Group group = new Group();

            group = group.Map(command);

            _groupRepository.Add(group);
        }

        public IList<Group> GetAll(int groupID)
        {
            var group = _groupRepository.GetAll(groupID);

            return new List<Group>(group);
        }

        public void Update(MaintenanceGroupCommand command)
        {
            Group group = new Group();

            group = group.Map(command);

            _groupRepository.Update(group);
        }

        public Result<Group> GetByID(int groupID)
        {
            var group = _groupRepository.GetByID(groupID);

            return Result.Ok<Group>(0, "", group);
        }

        public IPagedList<Group> GetAll(FilterGroupCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var group = _groupRepository.GetAll(command);

            return new PagedList<Group>(group, pageIndex, pageSize);
        }

        public void Delete(int groupID)
        {
            _groupRepository.Delete(groupID);
        }

        public string GetGroupNameByID(int groupID)
        {
            return _groupRepository.GetGroupNameByID(groupID);
        }
    }
}

