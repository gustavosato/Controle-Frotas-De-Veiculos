namespace ControleVeiculos.Domain.Command.Seguros
{
    public class FilterSeguroCommand
    {
        public string Apolice  { get; set; }
        public string VeiculoID { get; set; }
        public string Seguradora { get; set; }
        public string Franquia { get; set; }
        public string TipoSeguro { get; set; }
        public string DataContratacao { get; set; }
        public string Vigencia { get; set; }
        public string FimContratacao { get; set; }
        
    }
}
