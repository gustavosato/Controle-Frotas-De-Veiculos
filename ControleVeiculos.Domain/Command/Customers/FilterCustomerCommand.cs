namespace ControleVeiculos.Domain.Command.Customers
{
    public class FilterCustomerCommand
    {
        public string CustomerID {get; set;}
        public string CustomerName { get; set; }
        public string SegmentID { get; set; }
        public string TypeID { get; set; }
        public string UserID { get; set; }
        public bool IsActive { get; set; }
    }
}