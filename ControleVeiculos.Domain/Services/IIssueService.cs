using Lean.Test.Cloud.Domain.Command.Issues;
using Lean.Test.Cloud.Domain.Entities.Issues;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IIssueService : IDisposable
    {
        void Add(MaintenanceIssueCommand command);
        void Update(MaintenanceIssueCommand command);

        Result<Issue> GetByID(int issueID);
        IPagedList<Issue> GetAll(FilterIssueCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int IssueID);
    }
}
