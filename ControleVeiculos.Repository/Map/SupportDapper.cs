using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Supports")]
    public class SupportDapper
    {
        [ExplicitKey]
        public int supportID { get; set; }
        public string summary { get; set; }
        public string severityID { get; set; }
        public string statusID { get; set; }
        public string priorityID { get; set; }
        public string typeID { get; set; }
        public string assingToID { get; set; }
        public string resolutionDate { get; set; }
        public string description { get; set; }
        public string customerID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}
