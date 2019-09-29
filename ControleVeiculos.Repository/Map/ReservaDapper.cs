using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Reserva")]
    public class ReservaDapper
    {
        [ExplicitKey]
        public int reservaID { get; set; }
        public string dataReserva { get; set; }
        public string finalidade { get; set; }
        public string destino { get; set; }
        public string funcionarioID { get; set; }
        public string numeroCnh { get; set; }
        public string veiculoID { get; set; }
        
    }
}
