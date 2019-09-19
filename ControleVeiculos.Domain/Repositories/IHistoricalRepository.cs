using Lean.Test.Cloud.Domain.Command.Historicals;
using Lean.Test.Cloud.Domain.Entities.Historicals;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IHistoricalRepository
    {
        void Add(Historical historical);
        void Update(Historical historical);
        Historical GetByID(int systemFeatureID);
        List<Historical> GetAll(FilterHistoricalCommand command);
        void Delete(int systemFeatureID);
        void Delete(string systemFeatureID, int recordID);
    }
}
