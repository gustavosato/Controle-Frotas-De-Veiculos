using ControleVeiculos.Domain.Command.SystemFeatures;
using ControleVeiculos.Domain.Entities.SystemFeatures;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Repositories
{
    public interface ISystemFeatureRepository
    {
        void Add(SystemFeature systemFeature);
        void Update(SystemFeature systemFeature);
        SystemFeature GetByID(int systemFeatureID);
        List<SystemFeature> GetAll(FilterSystemFeatureCommand command);
        List<SystemFeature> GetAll();
        void Delete(int systemFeatureID);
    }
}
