using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("SystemParameters")]
    public class SystemParameterDapper
    {
        [ExplicitKey]
        public int parameterID { get; set; }
        public string paramterName { get; set; }
        public string paramterValue { get; set; }
        public string paramterDefaultValue { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}
