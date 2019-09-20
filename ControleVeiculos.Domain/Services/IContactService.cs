using ControleVeiculos.Domain.Command.Contacts;
using ControleVeiculos.Domain.Entities.Contacts;
using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface IContactService : IDisposable
    {
        void Add(MaintenanceContactCommand command);
        void Update(MaintenanceContactCommand command);
        Result<Contact> GetByID(int defecID);
        IPagedList<Contact> GetAll(FilterContactCommand command, int pageIndex = 0, int pageSize = int.MaxValue);
        IList<Contact> GetAll(int contactID, int customerID);
        void Delete(int defecID);
        string GetContactNameByID(int contatctID);
    }
}
