using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("ApplicationSystems")]
    public class ApplicationSystemDapper
    {
        [ExplicitKey]
        public int    applicationSystemID     {get; set;}
        public string applicationSystemName   {get; set;}
        public string description       {get; set;}
        public string applicationTypeID {get; set;}
        public string customerID { get; set;}
        public string createdByID        {get; set;}
        public string creationDate      {get; set;}
        public string modifiedByID      {get; set;}
        public string lastModifiedDate  {get; set;}
    }
}
