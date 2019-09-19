using Lean.Test.Cloud.Domain.Command.Issues;
using Lean.Test.Cloud.Domain.Entities.Issues;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IIssueRepository
    {
        void Add(Issue issue);
        void Update(Issue issue);
        Issue GetByID(int issueID);
        List<Issue> GetAll(FilterIssueCommand command);
        void Delete(int applicationSystemID);
    }
}
