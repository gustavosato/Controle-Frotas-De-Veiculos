namespace ControleVeiculos.Domain.Command.Clientes
{
    public class FilterClienteCommand
    {
        public string NomeCliente  { get; set; }
        public string Ramo { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Status { get; set; }
        
    }
}
