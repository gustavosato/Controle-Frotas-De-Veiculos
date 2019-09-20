namespace ControleVeiculos.Domain.Entities.Workflows
{
    public class Workflow
    {
        public int    workflowID       {get; set;}
        public string systemFeatureID         {get; set;}
        public string groupID          {get; set;}
        public string statusID         {get; set;}
        public string statusToID       {get; set;}
        public string createdByID       {get; set;}
        public string creationDate     {get; set;}
        public string modifiedByID     {get; set;}
        public string lastModifiedDate {get; set;}
    }
}

