using Lean.Test.Cloud.Domain.Command.PositionsSalaries;
using Lean.Test.Cloud.Domain.Entities.PositionsSalaries;
using System.Collections.Generic;


namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IPositionsSalarieRepository
    {
        void Add(PositionsSalarie positionsSalarie);
        void Update(PositionsSalarie positionsSalarie);
        PositionsSalarie GetByID(int positionsSalarieID);
        void Delete(int positionsSalarieID);
        List<PositionsSalarie> GetAll(FilterPositionsSalarieCommand command);
    }
}
