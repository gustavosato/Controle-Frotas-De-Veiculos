using Lean.Test.Cloud.Domain.Entities.Demands;
using Lean.Test.Cloud.Domain.Entities.Users;
using Lean.Test.Cloud.Domain.Entities.Licenses;

using System;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IExportManagerService : IDisposable
    {
        string ExportDemandXml(IList<Demand> demands);
        string ExportUserXml(IList<User> users);
        string ExportLicenseXml(string order, string license, string expirationDate, string typeLicense, string hostName, string status, string localKey);

    }
}
