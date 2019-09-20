namespace ControleVeiculos.Domain.Command.EquipmentAccessories
{
    public class MaintenanceEquipmentAccessorieCommand
    {
        public int EquipmentAccessorieID { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public string ModelName { get; set; }
        public string AssignToID { get; set; }
        public string TypeID { get; set; }
        public bool Invoicing { get; set; }
        public string AmountInvoicing { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
