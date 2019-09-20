namespace ControleVeiculos.Domain.Command.TimeReleases
{
    public class FilterTimeReleaseCommand
    {
        public string RegisterDate { get; set; }
        public string CreatedByID { get; set; }
        public string DemandID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ActivityID   { get; set; }
        public string CustomerID { get; set; }
        public string IsApproved { get; set; }

    }
}
