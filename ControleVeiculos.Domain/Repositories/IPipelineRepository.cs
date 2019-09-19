using Lean.Test.Cloud.Domain.Command.Pipelines;
using Lean.Test.Cloud.Domain.Entities.Pipelines;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
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
