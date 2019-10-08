namespace ControleVeiculos.Domain.Entities.Manutencoes
{
    public class Manutencao
    {
        public int manutencaoID { get; set; }
        public string responsavel { get; set; }
        public string dataManutencao { get; set; }
        public string descricao { get; set; }
        public string veiculoID { get; set; }
    }
}
