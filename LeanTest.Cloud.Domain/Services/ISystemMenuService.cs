using Lean.Test.Cloud.Domain.Command.SystemMenus;
using Lean.Test.Cloud.Domain.Entities.SystemMenus;
using System;
namespace Lean.Test.Cloud.Domain.Services
{
    public interface ISystemMenuService : IDisposable
    {
        void Add(MaintenanceSystemMenuCommand command);
        void Update(MaintenanceSystemMenuCommand command);
        Result<SystemMenu> GetByID(int menuID);
        IPagedList<SystemMenu> GetAll(FilterSystemMenuCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int menuID);
    }
}
