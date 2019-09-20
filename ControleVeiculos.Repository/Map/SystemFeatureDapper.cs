using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("SystemFeatures")]
    public class SystemFeatureDapper
    {
        [ExplicitKey]
        public int systemFeatureID { get; set; }
        public string systemFeatureName { get; set; }
        public string systemFeatureTypeID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}

