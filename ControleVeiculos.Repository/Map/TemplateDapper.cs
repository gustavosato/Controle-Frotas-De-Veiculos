using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("Templates")]
    public class TemplateDapper
    {
        [ExplicitKey]
        public int    templateID       {get; set;}
        public string templateName     {get; set;}
        public string description      {get; set;}
        public string domainID         {get; set;}
        public string createdByID       {get; set;}
        public string creationDate     {get; set;}
        public string modifiedByID     {get; set;}
        public string lastModifiedDate {get; set;}
    }
}
