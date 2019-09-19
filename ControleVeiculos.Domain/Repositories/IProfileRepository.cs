using Lean.Test.Cloud.Domain.Command.Profiles;
using Lean.Test.Cloud.Domain.Entities.Profiles;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IProfileRepository
    {
        void Add(Profile profile);
        void Update(Profile profile);
        Profile GetByID(int profileID);
        string GetAllow (FilterProfileCommand command);
        List<Profile> GetAll(FilterProfileCommand command);
        void Delete(int profileID);
    }
}
