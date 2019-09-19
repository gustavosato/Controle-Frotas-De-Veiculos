using Lean.Test.Cloud.Domain.Command.Workflows;
using Lean.Test.Cloud.Domain.Entities.Workflows;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IWorkflowRepository
    {
        void Add(Workflow workflow);
        void Update(Workflow workflow);
        Workflow GetByID(int workflowID);
        List<Workflow> GetAll(FilterWorkflowCommand command);
        void Delete(int workflowID);
    }
}
