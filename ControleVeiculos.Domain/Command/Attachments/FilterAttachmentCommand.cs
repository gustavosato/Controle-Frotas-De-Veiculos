namespace ControleVeiculos.Domain.Command.Attachments
{
    public class FilterAttachmentCommand
    {
        public string FileName { get; set; }
        public string Description { get; set; }
        public string RecordID { get; set; }
        public string SystemFeatureID { get; set; }
        public string CreatedByID { get; set; }
    }
}
