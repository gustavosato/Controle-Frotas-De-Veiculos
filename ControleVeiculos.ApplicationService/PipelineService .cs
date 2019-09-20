using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Pipelines;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Pipelines;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class PipelineService : BaseAppService, IPipelineService
    {
        private readonly IPipelineRepository _pipelineRepository;

        public PipelineService(IPipelineRepository pipelineRepository)
        {
            _pipelineRepository = pipelineRepository;
        }

        public string Add(MaintenancePipelineCommand command)
        {
            Pipeline pipeline = new Pipeline();

            pipeline = pipeline.Map(command);

           return _pipelineRepository.Add(pipeline);
        }

        public void Update(MaintenancePipelineCommand command)
        {
            Pipeline pipeline = new Pipeline();

            pipeline = pipeline.Map(command);

            _pipelineRepository.Update(pipeline);
        }

        public Result<Pipeline> GetByID(int oportunityID)
        {
            var pipeline = _pipelineRepository.GetByID(oportunityID);

            return Result.Ok<Pipeline>(0, "", pipeline);
        }

        public IPagedList<Pipeline> GetAll(FilterPipelineCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var pipeline = _pipelineRepository.GetAll(command);

            return new PagedList<Pipeline>(pipeline, pageIndex, pageSize);
        }

        public List<Pipeline> GetAllCodeByCustomerID(string customerID)
        {
            var pipeline = _pipelineRepository.GetAllCodeByCustomerID(customerID);

            return new List<Pipeline>(pipeline);
        }

        public void Delete(int oportunityID)
        {
            _pipelineRepository.Delete(oportunityID);
        }

        public string GetOportunityCodeByID(int pipelineID)
        {
            return _pipelineRepository.GetOportunityCodeByID(pipelineID);
        }
    }
}

