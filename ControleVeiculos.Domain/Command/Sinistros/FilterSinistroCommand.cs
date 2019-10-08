namespace ControleVeiculos.Domain.Command.Sinistros
{
    public class FilterSinistroCommand
    {
        public string Apolice  { get; set; }
        public string Franquia { get; set; }
        public string TipoSinistro { get; set; }

    }
}
