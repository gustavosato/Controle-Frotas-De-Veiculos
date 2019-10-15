namespace ControleVeiculos.Domain.Entities.Cnhs
{
    public class Cnh
    {
        public int cnhID { get; set; }
        public int numeroCnh { get; set; }
        public string nome { get; set; }
        public string validade { get; set; }
        public string categoria { get; set; }
        public string funcionarioID { get; set; }
    }
}

