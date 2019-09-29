namespace ControleVeiculos.Domain.Entities.Veiculos
{
    public class Veiculo
    {
        public int veiculoID { get; set; }
        public string modelo { get; set; }
        public string cor { get; set; }
        public string placa { get; set; }
        public string status { get; set; }
        public string ano { get; set; }
        public string numeroChassi { get; set; }
        public string motor { get; set; }
        public string manutencaoID { get; set; }
        public string abastecimentoID { get; set; }
    }
}
