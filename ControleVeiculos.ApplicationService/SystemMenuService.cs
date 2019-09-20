using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.SystemMenus;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.SystemMenus;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class SystemMenuService : BaseAppService, ISystemMenuService
    {
        private readonly ISystemMenuRepository _systemMenuRepository;

        public SystemMenuService(ISystemMenuRepository systemMenuRepository)
        {
            _systemMenuRepository = systemMenuRepository;
        }

        public void Add(MaintenanceSystemMenuCommand command)
        {
            SystemMenu systemMenu = new SystemMenu();

            systemMenu = systemMenu.Map(command);

            _systemMenuRepository.Add(systemMenu);
        }

        public void Update(MaintenanceSystemMenuCommand command)
        {
            SystemMenu systemMenu = new SystemMenu();

            systemMenu = systemMenu.Map(command);

            _systemMenuRepository.Update(systemMenu);
        }

        public Result<SystemMenu> GetByID(int menuID)
        {
            var systemMenu = _systemMenuRepository.GetByID(menuID);

            return Result.Ok<SystemMenu>(0, "", systemMenu);
        }

        public IPagedList<SystemMenu> GetAll(FilterSystemMenuCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var systemMenu = _systemMenuRepository.GetAll(command);

            return new PagedList<SystemMenu>(systemMenu, pageIndex, pageSize);
        }

        public IList<SystemMenu> GetAll(int menuID)
        {
            var systemMenu = _systemMenuRepository.GetAll(menuID);

            return new List<SystemMenu>(systemMenu);
        }

        public void Delete(int menuID)
        {
            _systemMenuRepository.Delete(menuID);
        }
    }
}

