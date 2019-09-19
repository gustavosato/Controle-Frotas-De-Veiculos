using Lean.Test.Cloud.Domain.Command.Licenses;
using Lean.Test.Cloud.Domain.Entities.Licenses;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Repositories
{
    public interface ILicenseRepository
    {
        void Add(License license);
        void Update(License license);
        License GetByID(int applicationID);
        List<License> GetAll(FilterLicenseCommand command);
        void Delete(int licenseID);
    }
}
