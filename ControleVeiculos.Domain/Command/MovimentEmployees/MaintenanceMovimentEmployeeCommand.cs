namespace ControleVeiculos.Domain.Command.MovimentEmployees
{
    public class MaintenanceMovimentEmployeeCommand
    {
        public int MovimentEmployeeID { get; set; }
        public string EmployeeID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StatusID { get; set; }
        public string MovimentEmployeeTypeID { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovedByID { get; set; }
        public string Description { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
