using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Dashboard")]
    public class DashboardDapper
    {
        [ExplicitKey]
        public int dashboardID { get; set; }
        public string item1 { get; set; }
        public string item2 { get; set; }
        public string item3 { get; set; }
        public string item4 { get; set; }
        public string item5 { get; set; }
        public string item6 { get; set; }
        public string item7 { get; set; }
        public string item8 { get; set; }
        public string item9 { get; set; }
        public string item10 { get; set; }
        public string item11 { get; set; }
        public string item12 { get; set; }
        public string item13 { get; set; }
        public string item14 { get; set; }
        public string item15 { get; set; }
        public string item16 { get; set; }
        public string item17 { get; set; }
        public string item18 { get; set; }
        public string item19 { get; set; }
        public string item20 { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}
