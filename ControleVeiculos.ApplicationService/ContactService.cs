﻿using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.Domain.Entities.Contacts;
using Lean.Test.Cloud.Domain.Repositories;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Contacts;
using System.Linq;
using System.Collections.Generic;
using Lean.Test.Cloud.SharedKernel.Common;

namespace Lean.Test.Cloud.ApplicationService
{
    public class ContactService : BaseAppService, IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public void Add(MaintenanceContactCommand command)
        {
            Contact contact = new Contact();

            contact = contact.Map(command);

            _contactRepository.Add(contact);
        }

        public void Update(MaintenanceContactCommand command)
        {
            Contact contact = new Contact();

            contact = contact.Map(command);

            _contactRepository.Update(contact);
        }

        public IList<Contact> GetAll(int contactID, int customerID)
        {
            var contact = _contactRepository.GetAll(contactID, customerID);

            return new List<Contact>(contact);
        }

        public Result<Contact> GetByID(int contactID)
        {
            var contact = _contactRepository.GetByID(contactID);

            return Result.Ok<Contact>(0, "", contact);
        }

        public IPagedList<Contact> GetAll(FilterContactCommand command, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var contact = _contactRepository.GetAll(command);

            return new PagedList<Contact>(contact, pageIndex, pageSize);
        }

        public void Delete(int contactID)
        {
            _contactRepository.Delete(contactID);
        }

        public string GetContactNameByID(int contactID)
        {
            return _contactRepository.GetContactNameByID(contactID);
        }
    }
}
