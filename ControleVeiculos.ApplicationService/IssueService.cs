using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Issues;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Issues;
using System.Linq;
using System.Collections.Generic;
using ControleVeiculos.SharedKernel.Common;

namespace ControleVeiculos.ApplicationService
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

