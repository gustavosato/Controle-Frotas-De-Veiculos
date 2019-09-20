using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.PipelineEvents;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.PipelineEvents;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class PipelineEventService : BaseAppService, IPipelineEventService
    {
        private readonly IPipelineEventRepository _pipelineEventRepository;

        public PipelineEventService(IPipelineEventRepository pipelineEventRepository)
        {
            _pipelineEventRepository = pipelineEventRepository;
        }

        public void Add(MaintenancePipelineEventCommand command)
        {
            PipelineEvent pipelineEvent = new PipelineEvent();

            pipelineEvent = pipelineEvent.Map(command);

            _pipelineEventRepository.Add(pipelineEvent);
        }

        public void Update(MaintenancePipelineEventCommand command)
        {
            PipelineEvent pipelineEvent = new PipelineEvent();

            pipelineEvent = pipelineEvent.Map(command);

            _pipelineEventRepository.Update(pipelineEvent);
        }

        public Result<PipelineEvent> GetByID(int saleEventID)
        {
            var pipelineEvent = _pipelineEventRepository.GetByID(saleEventID);

            return Result.Ok<PipelineEvent>(0, "", pipelineEvent);
        }

        public IPagedList<PipelineEvent> GetAll(FilterPipelineEventCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var pipelineEvent = _pipelineEventRepository.GetAll(command);

            return new PagedList<PipelineEvent>(pipelineEvent, pageIndex, pageSize);
        }

        public void Delete(int saleEventID)
        {
            _pipelineEventRepository.Delete(saleEventID);
        }
    }
}

