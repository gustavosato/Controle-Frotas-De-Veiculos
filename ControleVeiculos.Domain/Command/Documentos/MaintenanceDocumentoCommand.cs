namespace ControleVeiculos.Domain.Command.Documentos
{
    public class MaintenanceDocumentoCommand
    {
        public int DocumentoID { get; set; }
        public string SeguroID { get; set; }
        public string NumeroCnh { get; set; }
        public string ClienteID { get; set; }
        
    }
}
