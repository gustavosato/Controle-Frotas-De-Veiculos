using ControleVeiculos.Domain.Command.Profiles;
using ControleVeiculos.Domain.Entities.Profiles;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
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
