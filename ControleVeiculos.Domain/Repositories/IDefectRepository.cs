using ControleVeiculos.Domain.Command.Defects;
using ControleVeiculos.Domain.Entities.Defects;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IDefectRepository
    {
        string Add(Defect defect);
        void Update(Defect defect);
        Defect GetByID(int defectID);
        List<Defect> GetAll(FilterDefectCommand command);
        List<Defect> ApiGetAll();
        void Delete(int defectID);
    }
}
