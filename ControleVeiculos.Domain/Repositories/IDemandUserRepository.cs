using ControleVeiculos.Domain.Command.DemandsUsers;
using ControleVeiculos.Domain.Entities.DemandsUsers;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IDemandUserRepository
    {
        void Add(DemandUser DemandUser);
        void Delete(int demandID, int usersID);
    }
}
