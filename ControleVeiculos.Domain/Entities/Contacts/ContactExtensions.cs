using Lean.Test.Cloud.Domain.Command.Contacts;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Contacts
{
    public static class ContactExtensions
    {
        public static Result<Contact> GetMovimentEmployee(this Contact contact)
        {
            return Result.Ok(0, "", contact);
        }

        public static Contact Map(this Contact contact, MaintenanceContactCommand command)
        {

            contact.contactID = command.ContactID;
            contact.contactName = command.ContactName;
            contact.email = command.Email;
            contact.cellNumber = command.CellNumber;
            contact.telNumber = command.TelNumber;
            contact.functionID = command.FunctionID;
            contact.customerID = command.CustomerID;
            contact.description = command.Description;
            contact.feature = command.Feature;
            contact.createdByID = command.CreatedByID;
            contact.creationDate = command.CreationDate;
            contact.modifiedByID = command.ModifiedByID;
            contact.lastModifiedDate = command.LastModifiedDate;

            return contact;
        }
    }
}
