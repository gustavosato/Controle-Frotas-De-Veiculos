using ControleVeiculos.Domain.Command.Issues;
using ControleVeiculos.Domain.Entities.Issues;
using System;

namespace ControleVeiculos.Domain.Services
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
