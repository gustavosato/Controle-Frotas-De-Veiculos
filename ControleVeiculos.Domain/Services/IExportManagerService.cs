using ControleVeiculos.Domain.Entities.Demands;
using ControleVeiculos.Domain.Entities.Users;
using ControleVeiculos.Domain.Entities.Licenses;

using System;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface IExportManagerService : IDisposable
    {
        string ExportDemandXml(IList<Demand> demands);
        string ExportUserXml(IList<User> users);
        string ExportLicenseXml(string order, string license, string expirationDate, string typeLicense, string hostName, string status, string localKey);

    }
}
