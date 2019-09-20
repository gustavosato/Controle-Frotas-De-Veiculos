using ControleVeiculos.Domain;
using ControleVeiculos.Domain.Entities.Resumes;
using ControleVeiculos.Domain.Repositories;
using ControleVeiculos.Domain.Services;
using ControleVeiculos.Domain.Command.Resumes;

namespace ControleVeiculos.ApplicationService
{
    public class ResumeService : BaseAppService, IResumeService
    {
        private readonly IResumeRepository _resumeRepository;

        public ResumeService(IResumeRepository resumeRepository)
        {
            _resumeRepository = resumeRepository;
        }

        public string Add(MaintenanceResumeCommand command)
        {
            Resume resume = new Resume();

            resume = resume.Map(command);

         return _resumeRepository.Add(resume);
        }


        public void Update(MaintenanceResumeCommand command)
        {
            Resume resume = new Resume();

            resume = resume.Map(command);

            _resumeRepository.Update(resume);
        }

        public Result<Resume> GetByID(int resumeID)
        {
            var resume = _resumeRepository.GetByID(resumeID);

            return Result.Ok<Resume>(0, "", resume);
        }

        public IPagedList<Resume> GetAll(FilterResumeCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var resume = _resumeRepository.GetAll(command);

            return new PagedList<Resume>(resume, pageIndex, pageSize);
        }

        public void Delete(int resumeID)
        {
            _resumeRepository.Delete(resumeID);
        }
    }
}

