using Lean.Test.Cloud.Domain.Command.PositionsSalaries;
using Lean.Test.Cloud.Domain.Entities.PositionsSalaries;
using System;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IPositionsSalarieService : IDisposable
    {
        void Add(MaintenancePositionsSalarieCommand command);
        void Update(MaintenancePositionsSalarieCommand command);
        Result<PositionsSalarie> GetByID(int positionsSalarieID);
        IPagedList<PositionsSalarie> GetAll(FilterPositionsSalarieCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int positionsSalarieID);
    }
}
