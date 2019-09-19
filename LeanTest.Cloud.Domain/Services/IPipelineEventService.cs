using Lean.Test.Cloud.Domain.Command.PipelineEvents;
using Lean.Test.Cloud.Domain.Entities.PipelineEvents;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IPipelineEventService : IDisposable
    {
        void Add(MaintenancePipelineEventCommand command);
        void Update(MaintenancePipelineEventCommand command);
        Result<PipelineEvent> GetByID(int saleEventID);
        IPagedList<PipelineEvent> GetAll(FilterPipelineEventCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int saleEventID);
    }
}
