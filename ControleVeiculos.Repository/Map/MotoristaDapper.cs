using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Motorista")]
    public class MotoristaDapper
    {
        [ExplicitKey]
        public int motoristaID { get; set; }
        public string nomeMotorista { get; set; }
        public string empresa { get; set; }
        public string numeroCnh { get; set; }
         
    }
}
