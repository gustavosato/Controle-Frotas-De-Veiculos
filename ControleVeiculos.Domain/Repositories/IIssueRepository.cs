using ControleVeiculos.Domain.Command.Issues;
using ControleVeiculos.Domain.Entities.Issues;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
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
