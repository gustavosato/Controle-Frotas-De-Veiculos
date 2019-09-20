namespace ControleVeiculos.Domain.Command.Defects
{
    public class FilterDefectCommand
    {
        public string Summary { get; set; }
        public string StatusID { get; set; }
        public string SeverityID { get; set; }
        public string PriorityID { get; set; }
        public string AssingToID { get; set; }
        public string CreatedByID { get; set; }

    }
}
