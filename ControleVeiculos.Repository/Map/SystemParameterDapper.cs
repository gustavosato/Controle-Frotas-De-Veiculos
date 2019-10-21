using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("SystemParameter")]
    public class SystemParameterDapper
    {
        [ExplicitKey]
        public int parameterID { get; set; }
        public string paramterName { get; set; }
        public string paramterValue { get; set; }
        public string paramterDefaultValue { get; set; }
        
    }
}
