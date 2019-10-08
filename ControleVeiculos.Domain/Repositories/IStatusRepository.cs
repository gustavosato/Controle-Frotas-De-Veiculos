using ControleVeiculos.Domain.Command.Status;
using ControleVeiculos.Domain.Entities.Status;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IStatusRepository
    {
        void Add(Status status);
        void Update(Status status);
        Status GetByID(int statusID);
        List<Status> GetAll(FilterStatusCommand command);
        void Delete(int statusID);
    }
}
