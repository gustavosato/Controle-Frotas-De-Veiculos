using Lean.Test.Cloud.Domain.Command.Templates;
using Lean.Test.Cloud.Domain.Entities.Templates;
using System;

namespace Lean.Test.Cloud.Domain.Services
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
