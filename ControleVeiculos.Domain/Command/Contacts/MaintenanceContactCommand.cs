namespace Lean.Test.Cloud.Domain.Command.Contacts
{
    public class MaintenanceContactCommand
    {
        public int ContactID { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string CellNumber { get; set; }
        public string TelNumber { get; set; }
        public string FunctionID { get; set; }
        public string CustomerID { get; set; }
        public string Description { get; set; }
        public string Feature { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
