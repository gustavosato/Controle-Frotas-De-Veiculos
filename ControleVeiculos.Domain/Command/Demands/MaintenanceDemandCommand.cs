namespace ControleVeiculos.Domain.Command.Demands
{
    public class MaintenanceDemandCommand
    {
        public int DemandID { get; set; }
        public string DemandName { get; set; }
        public string TypeID { get; set; }
        public string StatusID { get; set; }
        public string Scope { get; set; }
        public string ServiceID { get; set; }
        public string ExternalCode { get; set; }
        public string DemandCode { get; set; }
        public string ResponsibleID { get; set; }
        public string AssignToTargetID { get; set; }
        public string PlanningStartDate { get; set; }
        public string PlanningEndDate { get; set; }
        public string ManagementEffort { get; set; }
        public string PlanningEffort { get; set; }
        public string ExecutionEffort { get; set; }
        public string Description { get; set; }
        public string CustomerID { get; set; }
        public string OportunityID { get; set; }
        public bool IsActive { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
