namespace ControleVeiculos.Domain.Entities.Rota
{
    public class Rota
    {
        public int rotaID { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string distancia { get; set; }
        public bool pedagio { get; set; }
        public string dataIda { get; set; }
        public string dataVolta { get; set; }
    }
}
