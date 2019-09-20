namespace ControleVeiculos.Domain.Entities.DailyLogs
{
    public class DailyLog
    {
        public int dailyLogID { get; set; }
        public string description { get; set; }
        public string demandID { get; set; }
        public bool isInternal { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}
