using ControleVeiculos.Domain.Command.Resumes;
using ControleVeiculos.Domain.Entities.Resumes;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
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
