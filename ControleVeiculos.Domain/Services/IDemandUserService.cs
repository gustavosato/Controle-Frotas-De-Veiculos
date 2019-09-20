using ControleVeiculos.Domain.Command.DemandsUsers;
using ControleVeiculos.Domain.Entities.DemandsUsers;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface IDemandUserService : IDisposable
    {
        void Add(MaintenanceDemandUserCommand command);
        void Delete(int demandID, int usersID);
    }
}
