using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("ParameterValue")]
    public class ParameterValueDapper
    {
        [ExplicitKey]
        public int parameterValueID { get; set; }
        public string parameterValue { get; set; }
        public string parameterID { get; set; }
        public string parentID { get; set; }
        public string isSystem { get; set; }
        public string description { get; set; }
        
    }
}
