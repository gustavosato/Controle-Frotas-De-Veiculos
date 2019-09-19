using Lean.Test.Cloud.Domain.Command.Resumes;
using Lean.Test.Cloud.Domain.Entities.Resumes;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IResumeService : IDisposable
    {
        string Add(MaintenanceResumeCommand command);
        void Update(MaintenanceResumeCommand command);
        Result<Resume> GetByID(int resumeID);
        IPagedList<Resume> GetAll(FilterResumeCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        void Delete(int resumeID);
    }
}
