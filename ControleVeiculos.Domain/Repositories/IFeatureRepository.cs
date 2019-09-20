using ControleVeiculos.Domain.Command.Features;
using ControleVeiculos.Domain.Entities.Features;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface IFeatureRepository
    {
        void Add(Feature feature);
        void Update(Feature feature);
        Feature GetByID(int featureID);
        List<Feature> GetAll(int userID, FilterFeatureCommand command);
        List<Feature> GetAll(string applicationSystemID);
        void Delete(int featureID);
        string GetFeatureNameByID(int featureID);

    }
}
