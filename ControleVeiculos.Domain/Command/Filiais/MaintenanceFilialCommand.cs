namespace ControleVeiculos.Domain.Command.Filiais
{
    public class MaintenanceFilialCommand
    {
        public int FilialID { get; set; }
        public string NomeFilial { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        
    }
}
