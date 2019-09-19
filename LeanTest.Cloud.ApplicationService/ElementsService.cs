using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Elements;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Elements;
using System.Linq;
using System.Collections.Generic;
using Lean.Test.Cloud.SharedKernel.Common;

namespace Lean.Test.Cloud.ApplicationService
{
    public class ElementService : BaseAppService, IElementService
    {
        private readonly IElementRepository _elementRepository;

        public ElementService(IElementRepository elementRepository)
        {
            _elementRepository = elementRepository;
        }

        public void Add(MaintenanceElementCommand command)
        {
            Element element = new Element();

            element = element.Map(command);

            _elementRepository.Add(element);
        }

        public void Update(MaintenanceElementCommand command)
        {
            Element element = new Element();

            element = element.Map(command);

            _elementRepository.Update(element);
        }

        public Result<Element> GetByID(int elementID)
        {
            var element = _elementRepository.GetByID(elementID);

            return Result.Ok<Element>(0, "", element);
        }

        public IPagedList<Element> GetAll(FilterElementCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var element = _elementRepository.GetAll(command);

            return new PagedList<Element>(element, pageIndex, pageSize);
        }

        public void Delete(int elementID)
        {
            _elementRepository.Delete(elementID);
        }
    }
}

