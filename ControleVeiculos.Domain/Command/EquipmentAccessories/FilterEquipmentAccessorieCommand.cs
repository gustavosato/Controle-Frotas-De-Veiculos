namespace ControleVeiculos.Domain.Command.EquipmentAccessories
{
    public class FilterEquipmentAccessorieCommand
    {
        public string EquipmentAccessorieName { get; set; }
        public string SerialNumber { get; set; }
        public string AssignToID { get; set; }
        public string TypeID { get; set; }
        public bool Invoicing { get; set; }
        public string CreatedByID { get; set; } 
    }
}
