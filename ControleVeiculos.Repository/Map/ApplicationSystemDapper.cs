using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("ApplicationSystem")]
    public class ApplicationSystemDapper
    {
        [ExplicitKey]
        public int applicationSystemID { get; set; }
        public string applicationSystemName { get; set; }
        public string description { get; set; }
        public string applicationTypeID { get; set; }
    }
}
