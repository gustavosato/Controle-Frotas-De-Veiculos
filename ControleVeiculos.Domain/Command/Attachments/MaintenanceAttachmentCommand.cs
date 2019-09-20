namespace ControleVeiculos.Domain.Command.Attachments
{
    public class MaintenanceAttachmentCommand
    {
        public int    AttachmentID         {get; set;}
        public string FileName         {get; set;}
        public string Description      {get; set;}
        public string BinaryFile       {get; set;}
        public string PathFile         {get; set;}
        public string SizeFile         {get; set;}
        public string RecordID         {get; set;}
        public string SystemFeatureID { get; set;}
        public string CreatedByID       {get; set;}
        public string CreationDate     {get; set;}
        public string ModifiedByID     {get; set;}
        public string LastModifiedDate {get; set;}
    }
}
