namespace ControleVeiculos.Domain.Command.Documentos
{
    public class FilterDocumentoCommand
    {
        public string SeguroID  { get; set; }
        public string ClienteID { get; set; }
        public string SegurosID { get; set; }
        public string NumeroCnh { get; set; }
    }
}
