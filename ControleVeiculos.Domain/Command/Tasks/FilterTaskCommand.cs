namespace ControleVeiculos.Domain.Command.Tasks
{
    public class FilterTaskCommand
    {
        public string Summary { get; set; }
        public string AssignToID { get; set; }
        public string StatusID { get; set; }
        public string CreatedByID { get; set; } 
    }
}
