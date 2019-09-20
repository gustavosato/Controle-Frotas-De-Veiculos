using ControleVeiculos.Domain.Command.Supports;
using ControleVeiculos.Domain.Entities.Supports;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface ISupportService : IDisposable
    {
        string Add(MaintenanceSupportCommand command);
        void Update(MaintenanceSupportCommand command);
        Result<Support> GetByID(int defecID);
        IPagedList<Support> GetAll(FilterSupportCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<Support> GetAll(int supportID, int customerID);
        void Delete(int supportD);
    }
}
