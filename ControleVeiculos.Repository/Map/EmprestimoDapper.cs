using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Emprestimo")]
    public class EmprestimoDapper
    {
        [ExplicitKey]
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

