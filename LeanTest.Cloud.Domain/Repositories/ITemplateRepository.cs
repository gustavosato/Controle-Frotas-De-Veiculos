using Lean.Test.Cloud.Domain.Command.Templates;
using Lean.Test.Cloud.Domain.Entities.Templates;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface ITemplateRepository
    {
        void Add(Template template);
        void Update(Template template);
        Template GetByID(int templateID);
        List<Template> GetAll(FilterTemplateCommand command);
        void Delete(int templateID);
    }
}
