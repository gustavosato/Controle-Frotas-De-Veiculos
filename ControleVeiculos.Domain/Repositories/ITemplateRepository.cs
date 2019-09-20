using ControleVeiculos.Domain.Command.Templates;
using ControleVeiculos.Domain.Entities.Templates;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
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
