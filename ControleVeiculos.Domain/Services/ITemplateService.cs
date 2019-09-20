using ControleVeiculos.Domain.Command.Templates;
using ControleVeiculos.Domain.Entities.Templates;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface ITemplateService : IDisposable
    {
        void Add(MaintenanceTemplateCommand command);
        void Update(MaintenanceTemplateCommand command);
        Result<Template> GetByID(int templateID);
        IPagedList<Template> GetAll(FilterTemplateCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int templateID);
    }
}
