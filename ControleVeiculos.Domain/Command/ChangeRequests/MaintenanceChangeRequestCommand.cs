namespace ControleVeiculos.Domain.Command.ChangeRequests
{
    public class MaintenanceChangeRequestCommand
    {
        public int    ChangeRequestID     {get; set;}
        public string Summary          {get; set;}
        public string ManagementEffort {get; set;}
        public string PlanningEffort   {get; set;}
        public string ExecutionEffort  {get; set;}
        public string StatusID         {get; set;}
        public string TargetDate       {get; set;}
        public string ApprovedDate     {get; set;}
        public string ApprovedByID { get; set; }
        public string Description      {get; set;}
        public string DemandID         {get; set;}
        public string RequestByID      {get; set;}
        public string CreatedByID       {get; set;}
        public string CreationDate     {get; set;}
        public string ModifiedByID     {get; set;}
        public string LastModifiedDate {get; set;}
    }
}

   