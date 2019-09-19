using Lean.Test.Cloud.Domain.Command.Contacts;
using Lean.Test.Cloud.Domain.Entities.Contacts;
using System.Collections.Generic;


namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface IContactRepository
    {
        void Add(Contact contact);
        void Update(Contact contact);
        Contact GetByID(int contactID);
        List<Contact> GetAll(FilterContactCommand command);
        List<Contact> GetAll(int contactID, int customerID);
        void Delete(int contactID);
        string GetContactNameByID(int contactID);
    }
}
