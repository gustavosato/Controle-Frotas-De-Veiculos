using ControleVeiculos.Domain.Command.PositionsSalaries;
using ControleVeiculos.Domain.Entities.PositionsSalaries;
using System.Collections.Generic;


namespace ControleVeiculos.Domain.Repositories
{
    public interface IPositionsSalarieRepository
    {
        void Add(PositionsSalarie positionsSalarie);
        void Update(PositionsSalarie positionsSalarie);
        PositionsSalarie GetByID(int positionsSalarieID);
        void Delete(int positionsSalarieID);
        List<PositionsSalarie> GetAll(FilterPositionsSalarieCommand command);
    }
}
