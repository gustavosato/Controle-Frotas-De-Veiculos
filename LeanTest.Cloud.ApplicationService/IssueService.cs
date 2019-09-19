using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Issues;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Issues;
using System.Linq;
using System.Collections.Generic;
using Lean.Test.Cloud.SharedKernel.Common;

namespace Lean.Test.Cloud.ApplicationService
{
    public class IssueService : BaseAppService, IIssueService
    {
        private readonly IIssueRepository _issueRepository;

        public IssueService(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public void Add(MaintenanceIssueCommand command)
        {
            Issue issue = new Issue();

            issue = issue.Map(command);

            _issueRepository.Add(issue);
        }

        public void Update(MaintenanceIssueCommand command)
        {
            Issue issue = new Issue();

            issue = issue.Map(command);

            _issueRepository.Update(issue);
        }

        public Result<Issue> GetByID(int issueID)
        {
            var issue = _issueRepository.GetByID(issueID);

            return Result.Ok<Issue>(0, "", issue);
        }

        public IPagedList<Issue> GetAll(FilterIssueCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var issue = _issueRepository.GetAll(command);

            return new PagedList<Issue>(issue, pageIndex, pageSize);
        }

        public void Delete(int issueID)
        {
            _issueRepository.Delete(issueID);
        }
    }
}

