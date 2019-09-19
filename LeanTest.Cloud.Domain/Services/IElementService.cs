using Lean.Test.Cloud.Domain.Command.Elements;
using Lean.Test.Cloud.Domain.Entities.Elements;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IElementService : IDisposable
    {
        void Add(MaintenanceElementCommand command);
        void Update(MaintenanceElementCommand command);
        Result<Element> GetByID(int elementID);
        IPagedList<Element> GetAll(FilterElementCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int elementID);
    }
}
