namespace ControleVeiculos.Domain.Command.DailyLogs
{
    public class FilterDailyLogCommand
    {
        public string Description { get; set; }
        public string DemandID { get; set; }
        public string CreatedByID { get; set; }
        public string UserID { get; set; }
        public string customerID { get; set; }
    }
}