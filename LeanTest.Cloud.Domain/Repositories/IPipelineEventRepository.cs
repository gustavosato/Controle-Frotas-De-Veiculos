using Lean.Test.Cloud.Domain.Command.PipelineEvents;
using Lean.Test.Cloud.Domain.Entities.PipelineEvents;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
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
