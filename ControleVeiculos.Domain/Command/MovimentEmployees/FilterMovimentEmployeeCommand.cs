namespace ControleVeiculos.Domain.Command.MovimentEmployees

{
    public class FilterMovimentEmployeeCommand
    {
        public string EmployeeID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StatusID { get; set; }
        public string MovimentEmployeeTypeID { get; set; }
    }
}
