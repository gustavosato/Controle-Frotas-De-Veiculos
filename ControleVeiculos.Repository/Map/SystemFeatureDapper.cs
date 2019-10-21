using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("SystemFeature")]
    public class SystemFeatureDapper
    {
        [ExplicitKey]
        public int systemFeatureID { get; set; }
        public string systemFeatureName { get; set; }
        public string systemFeatureTypeID { get; set; }
        
    }
}

