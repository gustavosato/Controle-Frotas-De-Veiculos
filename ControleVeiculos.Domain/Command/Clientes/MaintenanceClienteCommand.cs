namespace ControleVeiculos.Domain.Command.Clientes
{
    public class MaintenanceClienteCommand
    {
        public int ClienteID { get; set; }
        public string NomeCliente { get; set; }
        public string Ramo { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }

    }
}
