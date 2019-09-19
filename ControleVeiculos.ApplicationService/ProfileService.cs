using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Profiles;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Profiles;

namespace Lean.Test.Cloud.ApplicationService
{
    public class ProfileService : BaseAppService, IProfilesService
    {
        private readonly IProfileRepository _ProfileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _ProfileRepository = profileRepository;
        }

        public void Add(MaintenanceProfileCommand command)
        {
            Profile profile = new Profile();

            profile = profile.Map(command);

            _ProfileRepository.Add(profile);
        }

       public void Delete(int profileID)
        {
            _ProfileRepository.Delete(profileID);
        }

        public IPagedList<Profile> GetAll(FilterProfileCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var profileAll = _ProfileRepository.GetAll(command);

            return new PagedList<Profile>(profileAll, pageIndex, pageSize);
        }

        public Result<Profile> GetByID(int profileID)
        {
            var profile = _ProfileRepository.GetByID(profileID);

            return Result.Ok<Profile>(0, "", profile);
        }

        public string GetAllow(FilterProfileCommand command)
        {
            string profile = _ProfileRepository.GetAllow(command);

            return profile;
        }

        public void Update(MaintenanceProfileCommand command)
        {
            Profile profile = new Profile();

            profile = profile.Map(command);

            _ProfileRepository.Update(profile);
        }
    }
}
