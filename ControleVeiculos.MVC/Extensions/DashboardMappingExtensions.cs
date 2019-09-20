using ControleVeiculos.Domain.Entities.Dashboards;
using ControleVeiculos.MVC.Models.Dashboards;

namespace ControleVeiculos.MVC.Extensions
{
    public static class DashboardMappingExtensions
    {
        public static DashboardModel ToModel(this Dashboard entity)
        {
            if (entity == null)
                return null;

            var model = new DashboardModel
            {
                Item1 = entity.item1,
             
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}