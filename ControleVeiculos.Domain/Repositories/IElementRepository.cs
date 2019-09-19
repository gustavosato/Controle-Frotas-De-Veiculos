using Lean.Test.Cloud.Domain.Command.Elements;
using Lean.Test.Cloud.Domain.Entities.Elements;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
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
