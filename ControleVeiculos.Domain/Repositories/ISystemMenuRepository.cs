using Lean.Test.Cloud.Domain.Command.SystemMenus;
using Lean.Test.Cloud.Domain.Entities.SystemMenus;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface ISystemMenuRepository
    {
        void Add(SystemMenu systemMenu);
        void Update(SystemMenu systemMenu);
        SystemMenu GetByID(int menuID);
        List<SystemMenu> GetAll(FilterSystemMenuCommand command);
        List<SystemMenu> GetAll(int menuID);
        void Delete(int menuID);
    }
}
