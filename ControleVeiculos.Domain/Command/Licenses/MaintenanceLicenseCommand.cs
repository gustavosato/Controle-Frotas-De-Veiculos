namespace Lean.Test.Cloud.Domain.Command.Licenses
{
    public class MaintenanceLicenseCommand
    {
        public int LicenseID { get; set; }
        public string LicenseCode { get; set; }
        public string CustomerID { get; set; }
        public string ExpirationDate { get; set; }
        public string LicenseTypeID { get; set; }
        public string HostName { get; set; }
        public string MacAddress { get; set; }
        public string Description { get; set; }
        public string License { get; set; }
        public string ApprovedByID { get; set; }
        public string ApprovedDate { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
