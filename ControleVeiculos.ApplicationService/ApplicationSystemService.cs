using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.ApplicationSystems;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.ApplicationSystems;
using System.Collections.Generic;

namespace ControleVeiculos.ApplicationService
{
    public class ApplicationSystemService : BaseAppService, IApplicationSystemService
    {
        private readonly IApplicationSystemRepository _applicationSystemRepository;

        public ApplicationSystemService(IApplicationSystemRepository applicationSystemRepository)
        {
            _applicationSystemRepository = applicationSystemRepository;
        }

        public void Add(MaintenanceApplicationSystemCommand command)
        {
            ApplicationSystem applicationSystem = new ApplicationSystem();

            applicationSystem = applicationSystem.Map(command);

            _applicationSystemRepository.Add(applicationSystem);
        }

        public void Update(MaintenanceApplicationSystemCommand command)
        {
            ApplicationSystem applicationSystem = new ApplicationSystem();

            applicationSystem = applicationSystem.Map(command);

            _applicationSystemRepository.Update(applicationSystem);
        }

        public Result<ApplicationSystem> GetByID(int applicationSystemID)
        {
            var applicationSystem = _applicationSystemRepository.GetByID(applicationSystemID);

            return Result.Ok<ApplicationSystem>(0, "", applicationSystem);
        }

        public IPagedList<ApplicationSystem> GetAll(FilterApplicationSystemCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var applicationSystem = _applicationSystemRepository.GetAll(command);

            return new PagedList<ApplicationSystem>(applicationSystem, pageIndex, pageSize);
        }

        public IList<ApplicationSystem> GetAll(int userID)
        {
            var user = _applicationSystemRepository.GetAll(userID);

            return new List<ApplicationSystem>(user);
        }

        public void Delete(int applicationSystemID)
        {
            _applicationSystemRepository.Delete(applicationSystemID);
        }

        public string GetApplicationSystemNameByID(int applicationSystemID)
        {
            return _applicationSystemRepository.GetApplicationSystemNameByID(applicationSystemID);
        }
    }
}

