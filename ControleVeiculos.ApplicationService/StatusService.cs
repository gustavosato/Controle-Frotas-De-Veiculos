using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Status;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Status;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class StatusService : BaseAppService, IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public void Add(MaintenanceStatusCommand command)
        {
            Status status = new Status();

            status = status.Map(command);

            _statusRepository.Add(status);
        }

        public void Update(MaintenanceStatusCommand command)
        {
            Status status = new Status();

            status = status.Map(command);

            _statusRepository.Update(status);
        }

        public IList<Status> GetAll(int statusID)
        {
            var status = _statusRepository.GetAll(statusID);

            return new List<Status>(status);
        }

        public Result<Status> GetByID(int statusID)
        {
            var status = _statusRepository.GetByID(statusID);

            return Result.Ok<Status>(0, "", status);
        }

        public IPagedList<Status> GetAll(FilterStatusCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var status = _statusRepository.GetAll(command);

            return new PagedList<Status>(status, pageIndex, pageSize);
        }

        public void Delete(int statusID)
        {
            _statusRepository.Delete(statusID);
        }
       
    }
}

