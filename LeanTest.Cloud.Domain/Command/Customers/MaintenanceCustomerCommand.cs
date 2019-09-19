namespace Lean.Test.Cloud.Domain.Command.Customers
{
    public class MaintenanceCustomerCommand
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
        public string SegmentID { get; set; }
        public string TypeID { get; set; }
        public string Site { get; set; }
        public string Address { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
