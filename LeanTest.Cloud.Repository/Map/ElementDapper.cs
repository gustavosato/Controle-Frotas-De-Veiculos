using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("Elements")]
    public class ElementDapper
    {
        [ExplicitKey]
        public int    elementID            {get; set;}
        public string element              {get; set;}
        public string actionID             {get; set;}
        public string defaultValue         {get; set;}
        public string valuePerKilometer    {get; set;}
        public string domains              {get; set;}
        public string automationID         {get; set;}
        public string typeIdentificationID {get; set;}
        public string featureID            {get; set;}
        public string createdByID           {get; set;}
        public string creationDate         {get; set;}
        public string modifiedByID         {get; set;}
        public string lastModifiedDate     {get; set;}
    }
}

