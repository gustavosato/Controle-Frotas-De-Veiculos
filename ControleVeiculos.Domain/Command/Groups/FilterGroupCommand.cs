namespace ControleVeiculos.Domain.Command.Groups
{
    public class FilterGroupCommand
    {
        public string UserID { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
    }
}