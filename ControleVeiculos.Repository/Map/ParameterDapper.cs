using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Parameter")]
    public class ParameterDapper
    {
        [ExplicitKey]
        public int parameterID { get; set; }
        public string parameterName { get; set; }
        public string systemFeatureID { get; set; }
        
    }
}
