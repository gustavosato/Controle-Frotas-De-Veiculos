namespace ControleVeiculos.Domain.Entities.Cliente
{
    public class Cliente
    {
        public int clienteID { get; set; }
        public string nomeCliente { get; set; }
        public string ramo { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public string status { get; set; }
    }
}
