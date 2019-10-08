using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Parameters")]
    public class ParameterDapper
    {
        [ExplicitKey]
        public int parameterID { get; set; }
        public string parameterName { get; set; }
        public string systemFeatureID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}
