namespace ControleVeiculos.Domain.Command.Licenses
{
    public class FilterLicenseCommand
    {
        public string LicenseCode { get; set; }
        public string CustomerID { get; set; }
        public string HostName { get; set; }
        public string CreatedByID { get; set; }


    }
}