using Lean.Test.Cloud.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Lean.Test.Cloud.Domain.Entities.Demands;
using Lean.Test.Cloud.Domain.Entities.Users;

namespace Lean.Test.Cloud.ApplicationService
{
    public class ExportManagerService : BaseAppService, IExportManagerService
    {

        public string ExportDemandXml(IList<Demand> demands)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Demands");
            xmlWriter.WriteAttributeString("Version", "1.0.0.0");


            foreach (Demand demand in demands)
            {
                xmlWriter.WriteStartElement("Demand");
                xmlWriter.WriteElementString("DemandID", null, demand.demandID.ToString());
                xmlWriter.WriteElementString("DemandName", null, demand.demandName);
                xmlWriter.WriteElementString("DemandCode", null, demand.demandCode);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
        }

        public string ExportUserXml(IList<User> users)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Users");
            xmlWriter.WriteAttributeString("Version", "1.0.0.0");


            foreach (User user in users)
            {
                xmlWriter.WriteStartElement("User");
                xmlWriter.WriteElementString("UserID", null, user.userID.ToString());
                xmlWriter.WriteElementString("UserName", null, user.userName);
                xmlWriter.WriteElementString("Email", null, user.email);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
        }

        public string ExportLicenseXml(string order, string license, string expirationDate, string typeLicense, string hostName, string status, string localKey)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);

            xmlWriter.WriteStartDocument();
            //xmlWriter.WriteStartElement("License");
            //xmlWriter.WriteAttributeString("Version", "1.0.0.0");

            xmlWriter.WriteStartElement("configuration");
            xmlWriter.WriteElementString("product", null, "LeanTest Automation");
            xmlWriter.WriteElementString("company", null, "LeanTest");
            xmlWriter.WriteElementString("order", null, order);
            xmlWriter.WriteElementString("license", null, license);
            xmlWriter.WriteElementString("expirationDate", null, expirationDate);
            xmlWriter.WriteElementString("typeLicense", null, typeLicense);
            xmlWriter.WriteElementString("hostName", null, hostName);
            xmlWriter.WriteElementString("status", null, status);
            xmlWriter.WriteElementString("localKey", null, localKey);

            //xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
        }
    }
}
