namespace ControleVeiculos.Domain.Command.Status
{
    public class FilterStatusCommand
    {
        public string Disponibilidade  { get; set; }
        public bool EmUso { get; set; }
        public bool EmManutencao { get; set; }
        public bool Reservado { get; set; }
    }
}
