using Lean.Test.Cloud.Domain.Command.Workflows;
using Lean.Test.Cloud.Domain.Entities.Workflows;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IWorkflowService : IDisposable
    {
        void Add(MaintenanceWorkflowCommand command);
        void Update(MaintenanceWorkflowCommand command);
        Result<Workflow> GetByID(int workflowID);
        IPagedList<Workflow> GetAll(FilterWorkflowCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int workflowID);
    }
}
