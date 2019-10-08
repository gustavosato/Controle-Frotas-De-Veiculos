namespace ControleVeiculos.Domain.Command.Manutencoes
{
    public class MaintenanceManutencaoCommand
    {
        public int ManutencaoID { get; set; }
        public string Responsavel { get; set; }
        public string DataManutencao { get; set; }
        public string Descricao { get; set; }
        public string VeiculoID { get; set; }
        
    }
}
