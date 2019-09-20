using ControleVeiculos.Domain.Command.Workflows;
using ControleVeiculos.Domain.Entities.Workflows;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
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
