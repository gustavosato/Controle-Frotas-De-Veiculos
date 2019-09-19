using Lean.Test.Cloud.Domain.Command.Resumes;
using Lean.Test.Cloud.Domain.Entities.Resumes;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IResumeRepository
    {
        string Add(Resume resume);
        void Update(Resume resume);
        Resume GetByID(int resumeID);
        List<Resume> GetAll(FilterResumeCommand command);
        void Delete(int resumeID);
    }
}
