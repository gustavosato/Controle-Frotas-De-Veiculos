namespace ControleVeiculos.Domain.Command.Supports
{
    public class FilterSupportCommand
    {
        public string Summary { get; set; }
        public string SeverityID { get; set; }
        public string PriorityID { get; set; } 
        public string TypeID { get; set; } 
        public string StatusID { get; set; }
        public string AssingToID { get; set; } 
        public string CustomerID { get; set; } 
        public string CreatedByID { get; set; } 

    }
}
