﻿using ControleVeiculos.Domain.Entities.Contacts;
using ControleVeiculos.MVC.Models.Contacts;

namespace ControleVeiculos.MVC.Extensions
{
    public static class ContactMappingExtensions
    {
        public static ContactModel ToModel(this Contact entity)
        {
            if (entity == null)
                return null;

            var model = new ContactModel
            {
                ContactID = entity.contactID,
                ContactName = entity.contactName,
                Email = entity.email,
                CellNumber = entity.cellNumber,
                TelNumber = entity.telNumber,
                FunctionID = entity.functionID,
                CustomerID = entity.customerID,
                Description = entity.description,
                Feature = entity.feature,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}