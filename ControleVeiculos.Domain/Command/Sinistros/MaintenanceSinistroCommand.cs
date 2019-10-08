namespace ControleVeiculos.Domain.Command.Sinistros
{
    public class MaintenanceSinistroCommand
    {
        public int SinistroID { get; set; }
        public string Apolice { get; set; }
        public string Franquia { get; set; }
        public string TipoSinistro { get; set; }
        
    }
}
