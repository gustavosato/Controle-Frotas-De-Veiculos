namespace Lean.Test.Cloud.Domain.Command.Issues

{
    public class FilterIssueCommand
    {
        public string Summary { get; set; }
        public string StatusID { get; set; }
        public string CreatedByIDseverityID { get; set; }
        public string PriorityID { get; set; }
        public string AssingToID { get; set; }
        public string TypeID { get; set; }
        public string ResolutionID { get; set; }
        public string ResolutionDate { get; set; }
        public string CreatedByID { get; set; }
    }
}
