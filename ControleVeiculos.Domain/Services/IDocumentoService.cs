using ControleVeiculos.Domain.Command.Documentos;
using ControleVeiculos.Domain.Entities.Documentos;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IDocumentoService : IDisposable
    {
        void Add(MaintenanceDocumentoCommand command);
        void Update(MaintenanceDocumentoCommand command);
        Result<Documento> GetByID(int documentoID);
        IPagedList<Documento> GetAll(FilterDocumentoCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int documentoID);
    }
}
