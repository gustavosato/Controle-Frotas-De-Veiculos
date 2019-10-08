namespace ControleVeiculos.Domain.Command.Rotas
{
    public class MaintenanceRotaCommand
    {
        public int RotaID { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Distancia { get; set; }
        public bool Pedagio { get; set; }
        public string DataIda { get; set; }
        public string DataVolta { get; set; }

    }
}
