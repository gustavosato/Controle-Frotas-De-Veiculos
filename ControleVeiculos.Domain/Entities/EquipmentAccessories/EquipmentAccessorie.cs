namespace Lean.Test.Cloud.Domain.Entities.EquipmentAccessories
{
    public class EquipmentAccessorie
    {
        public int equipmentAccessorieID { get; set; }
        public string equipmentAccessorieName { get; set; }
        public string description { get; set; }
        public string serialNumber { get; set; }
        public string modelName { get; set; }
        public string assignToID { get; set; }
        public string typeID { get; set; }
        public bool invoicing { get; set; }
        public string amountInvoicing { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }

    }
}
