namespace ControleVeiculos.Domain.Command.Demands
{
    public class FilterDemandCommand
    {
        public string DemandID { get; set; }
        public string DemandName { get; set; }
        public string StatusID { get; set; }
        public string PlanningStartDate { get; set; }
        public string PlanningEndDate { get; set; }
        public string TypeID { get; set; }
        public string ServiceID { get; set; }
        public string DemandCode { get; set; }
        public string ExternalCode { get; set; }
        public bool IsActive { get; set; }
        public string ResponsibleID { get; set; }
        public string AssignToTargetID { get; set; }
        public string RegisterDateFrom { get; set; }
        public string RegisterDateTo { get; set; }
        public string CreatedByID { get; set; }
    }
}