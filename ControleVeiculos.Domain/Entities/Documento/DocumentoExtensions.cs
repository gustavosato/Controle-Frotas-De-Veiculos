using ControleVeiculos.Domain.Command.Documentos;
using System;

namespace ControleVeiculos.Domain.Entities.Documento
{
    public static class DocumentoExtensions
    {
        public static Result<Documento> GetDocumento(this Documento documento)
        {
            return Result.Ok(0, "", documento);
        }

        public static Documento Map(this Documento documento, MaintenanceDocumentoCommand command)
        {

            documento.documentoID = command.DocumentoID;
            documento.seguroID = command.SeguroID;
            documento.numeroCnh = command.NumeroCnh;
            documento.clienteID = command.ClienteID;
            
            return documento;
        }
    }
}
