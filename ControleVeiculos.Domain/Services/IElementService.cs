using ControleVeiculos.Domain.Command.Elements;
using ControleVeiculos.Domain.Entities.Elements;
using System;

namespace ControleVeiculos.Domain.Services
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
