using Lean.Test.Cloud.Domain.Command.Supports;
using Lean.Test.Cloud.Domain.Entities.Supports;
using System.Collections.Generic;


namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface ISupportRepository
    {
        string Add(Support support);
        void Update(Support support);
        Support GetByID(int supportID);
        List<Support> GetAll(FilterSupportCommand command);
        List<Support> GetAll(int supportID, int customerID);
        void Delete(int supportID);
    }
}
