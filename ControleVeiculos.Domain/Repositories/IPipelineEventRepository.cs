using ControleVeiculos.Domain.Command.PipelineEvents;
using ControleVeiculos.Domain.Entities.PipelineEvents;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IPipelineEventRepository
    {
        void Add(PipelineEvent pipelineEvent);
        void Update(PipelineEvent pipelineEvent);
        PipelineEvent GetByID(int saleEventID);
        List<PipelineEvent> GetAll(FilterPipelineEventCommand command);
        void Delete(int saleEventID);
    }
}
