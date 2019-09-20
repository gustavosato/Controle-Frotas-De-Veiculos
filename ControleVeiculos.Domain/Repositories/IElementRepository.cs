using ControleVeiculos.Domain.Command.Elements;
using ControleVeiculos.Domain.Entities.Elements;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IElementRepository
    {
        void Add(Element element);
        void Update(Element element);
        Element GetByID(int elementID);
        List<Element> GetAll(FilterElementCommand command);
        void Delete(int applicationID);
    }
}
