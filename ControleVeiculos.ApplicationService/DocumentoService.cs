using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Documentos;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Documentos;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
{
    public class DocumentoService : BaseAppService, IDocumentoService
    {
        private readonly IDocumentoRepository _documentoRepository;

        public DocumentoService(IDocumentoRepository documentoRepository)
        {
            _documentoRepository = documentoRepository;
        }

        public void Add(MaintenanceDocumentoCommand command)
        {
            Documento documento = new Documento();

            documento = documento.Map(command);

            _documentoRepository.Add(documento);
        }

        public void Update(MaintenanceDocumentoCommand command)
        {
            Documento documento = new Documento();

            documento = documento.Map(command);

            _documentoRepository.Update(documento);
        }

        //public IList<Documento> GetAll(int documentoID)
        //{
        //    var documento = _documentoRepository.GetAll(documentoID);

        //    return new List<Documento>(documento);
        //}

        public Result<Documento> GetByID(int documentoID)
        {
            var documento = _documentoRepository.GetByID(documentoID);

            return Result.Ok<Documento>(0, "", documento);
        }

        public IPagedList<Documento> GetAll(FilterDocumentoCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var documento = _documentoRepository.GetAll(command);

            return new PagedList<Documento>(documento, pageIndex, pageSize);
        }

        public void Delete(int documentoID)
        {
            _documentoRepository.Delete(documentoID);
        }
       
    }
}

