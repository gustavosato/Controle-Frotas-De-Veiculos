namespace Lean.Test.Cloud.Domain.Command.Templates
{
    public class MaintenanceTemplateCommand
    {
        public int    TemplateID       {get; set;}
        public string TemplateName     {get; set;}
        public string Description      {get; set;}
        public string DomainID         {get; set;}
        public string CreatedByID       {get; set;}
        public string CreationDate     {get; set;}
        public string ModifiedByID     {get; set;}
        public string LastModifiedDate {get; set;}
    }
}
