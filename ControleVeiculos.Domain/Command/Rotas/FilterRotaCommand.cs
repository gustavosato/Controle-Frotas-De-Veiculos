namespace ControleVeiculos.Domain.Command.Rotas
{
    public class FilterRotaCommand
    {
        public string Cidade  { get; set; }
        public string Estado { get; set; }
        public string DataIda { get; set; }
        public string DataVolta { get; set; }
        public string Pedagio { get; set; }

    }
}
