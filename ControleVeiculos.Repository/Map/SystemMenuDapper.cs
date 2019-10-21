using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("SystemMenu")]
    public class SystemMenuDapper
    {
        [ExplicitKey]
        public int menuID { get; set; }
        public string textMenu { get; set; }
        public string description { get; set; }
        public string ordem { get; set; }
        public string urlAction { get; set; }
        public string controller { get; set; }
        public string icon { get; set; }
        public bool itsAdmin { get; set; }
        public string systemFeatureID { get; set; }
    }
}

