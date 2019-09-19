using Lean.Test.Cloud.Domain.Command.Defects;
using Lean.Test.Cloud.Domain.Entities.Defects;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IDefectRepository
    {
        string Add(Defect defect);
        void Update(Defect defect);
        Defect GetByID(int defectID);
        List<Defect> GetAll(FilterDefectCommand command);
        List<Defect> ApiGetAll();
        void Delete(int defectID);
    }
}
