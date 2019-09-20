namespace ControleVeiculos.Domain.Command.Pipelines
{
    public class FilterPipelineCommand
    {
        public string CustomerID { get; set; }
        public string PriorityID { get; set; }
        public string FaseID { get; set; }
        public string OwnerID { get; set; }
        public string SaleManagerID { get; set; }
        public string PreSalesID { get; set; }
        public string OperationManagerID { get; set; }
        public string TypeID { get; set; }
        public string CostCenterID { get; set; }
        public string OfferID { get; set; }
        public string StatusID { get; set; }
    }
}
