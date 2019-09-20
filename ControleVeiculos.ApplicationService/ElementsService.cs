using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Elements;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Elements;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
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

