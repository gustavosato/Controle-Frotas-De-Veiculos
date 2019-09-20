using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.SystemParameters;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.SystemParameters;
using System.Collections.Generic;

namespace ControleVeiculos.ApplicationService
{
    public class SystemParameterService : BaseAppService, ISystemParameterService
    {
        private readonly ISystemParameterRepository _systemParameterRepository;

        public SystemParameterService(ISystemParameterRepository systemParameterRepository)
        {
            _systemParameterRepository = systemParameterRepository;
        }

        public void Add(MaintenanceSystemParameterCommand command)
        {
            SystemParameter systemParameter = new SystemParameter();

            systemParameter = systemParameter.Map(command);

            _systemParameterRepository.Add(systemParameter);
        }

        public void Update(MaintenanceSystemParameterCommand command)
        {
            SystemParameter systemParameter = new SystemParameter();

            systemParameter = systemParameter.Map(command);

            _systemParameterRepository.Update(systemParameter);
        }

        public Result<SystemParameter> GetByID(int systemParameterID)
        {
            var systemParameter = _systemParameterRepository.GetByID(systemParameterID);

            return Result.Ok<SystemParameter>(0, "", systemParameter);
        }

        public IPagedList<SystemParameter> GetAll(FilterSystemParameterCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var systemParameter = _systemParameterRepository.GetAll(command);

            return new PagedList<SystemParameter>(systemParameter, pageIndex, pageSize);
        }

        public void Delete(int systemParameterID)
        {
            _systemParameterRepository.Delete(systemParameterID);
        }
    }
}

