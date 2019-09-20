namespace ControleVeiculos.Domain.Command.Workflows
{
    public class MaintenanceWorkflowCommand
    {
        public int    WorkflowID       {get; set;}
        public string SystemFeatureID         {get; set;}
        public string GroupID          {get; set;}
        public string StatusID         {get; set;}
        public string StatusToID       {get; set;}
        public string CreatedByID       {get; set;}
        public string CreationDate     {get; set;}
        public string ModifiedByID     {get; set;}
        public string LastModifiedDate {get; set;}
    }
}
