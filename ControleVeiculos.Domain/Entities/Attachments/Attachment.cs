namespace Lean.Test.Cloud.Domain.Entities.Attachments
{
    public class Attachment
    {
        public int attachmentID { get; set; }
        public string fileName { get; set; }
        public string description { get; set; }
        public string binaryFile { get; set; }
        public string pathFile { get; set; }
        public string sizeFile { get; set; }
        public string recordID { get; set; }
        public string systemFeatureID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}
