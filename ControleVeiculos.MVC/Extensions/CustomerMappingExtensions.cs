using ControleVeiculos.Domain.Entities.Customers;
using ControleVeiculos.MVC.Models.Customers;

namespace ControleVeiculos.MVC.Extensions
{
    public static class CustomerMappingExtensions
    {
        public static CustomerModel ToModel(this Customer entity)
        {
            if (entity == null)
                return null;

            var model = new CustomerModel
            {
                CustomerID = entity.customerID,
                CustomerName = entity.customerName,
                Description = entity.description,
                IsActive = entity.isActive,
                SegmentID = entity.segmentID,
                TypeID = entity.typeID,
                Site = entity.site,
                Address = entity.address,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}