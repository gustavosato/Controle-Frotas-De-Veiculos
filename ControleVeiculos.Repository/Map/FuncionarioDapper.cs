using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Funcionarios")]
    public class FuncionarioDapper
    {
        [ExplicitKey]
        public int funcionarioID { get; set; }
        public string nomeFuncionario { get; set; }
        public string endereco { get; set; }
        public string cpf { get; set; }
        public string funcao { get; set; }
        public string setor { get; set; }
        public string telefone { get; set; }
        public string numeroCnh { get; set; }
    }
}
