namespace Lean.Test.Cloud.Domain.Command.ChangeRequests
{
    public class FilterChangeRequestCommand
    {
        public string Summary { get; set; }
        public string StatusID { get; set; }
        public string RequestByID { get; set; }
    }
}
