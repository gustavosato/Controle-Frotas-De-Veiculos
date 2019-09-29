using ControleVeiculos.Domain.Command.Documentos;
using ControleVeiculos.Domain.Entities.Documentos;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IDocumentoRepository
    {
        void Add(Documento documento);
        void Update(Documento documento);
        Documento GetByID(int documentoID);
        List<Documento> GetAll(FilterDocumentoCommand command);
        void Delete(int documentoID);
    }
}
