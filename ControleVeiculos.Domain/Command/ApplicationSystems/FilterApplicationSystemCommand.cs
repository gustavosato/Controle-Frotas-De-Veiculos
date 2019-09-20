namespace ControleVeiculos.Domain.Command.ApplicationSystems
{
    public class FilterApplicationSystemCommand
    {
        public string ApplicationSystemName { get; set; }
        public string ApplicationSystemTypeID { get; set; }
        public string CreatedByID { get; set; }
    }
}
