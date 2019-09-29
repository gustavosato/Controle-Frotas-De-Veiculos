namespace ControleVeiculos.Domain.Entities.Emprestimo
{
    public class Emprestimo
    {
        public int emprestimoID { get; set; }
        public string kmInicial { get; set; }
        public string kmFinal { get; set; }
        public string dataSaida { get; set; }
        public string dataRetorno { get; set; }
        public string destino { get; set; }
        public string veiculoID { get; set; }
        public string funcionarioID { get; set; }
    }
}
