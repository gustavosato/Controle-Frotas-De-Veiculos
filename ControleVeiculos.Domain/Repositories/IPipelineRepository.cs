using ControleVeiculos.Domain.Command.Pipelines;
using ControleVeiculos.Domain.Entities.Pipelines;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IPipelineRepository
    {
        string Add(Pipeline pipeline);
        void Update(Pipeline pipeline);
        Pipeline GetByID(int oportunityID);
        List<Pipeline> GetAll(FilterPipelineCommand command);
        List<Pipeline> GetAllCodeByCustomerID(string customerID);
        void Delete(int oportunityID);
        string GetOportunityCodeByID(int oportunityID);
    }
}
