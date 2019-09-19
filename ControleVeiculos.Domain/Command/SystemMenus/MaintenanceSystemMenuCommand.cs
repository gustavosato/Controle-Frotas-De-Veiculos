namespace Lean.Test.Cloud.Domain.Command.SystemMenus
{
    public class MaintenanceSystemMenuCommand
    {
        public int MenuID { get; set;}
        public string TextMenu { get; set; }
        public string Description { get; set; }
        public string Ordem { get; set; }
        public string UrlAction { get; set; }
        public string Controller { get; set; }
        public string Icon { get; set; }
        public bool ItsAdmin { get; set; }
        public string SystemFeatureID { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; } 
    }
}
