using System;
using Lean.Test.Cloud.Domain.Services;

namespace Lean.Test.Cloud.ApplicationService
{
    public class LicenseGeneratorService : BaseAppService, ILicenseGeneratorService
    {
        private readonly IExportManagerService _exportManagerService;
        private readonly IEncryptService _encryptService;
        private readonly ILicenseService _licenseService;

        public LicenseGeneratorService(IExportManagerService exportManagerService,
                                       IEncryptService encryptService,
                                       ILicenseService licenseService)
        {
            _exportManagerService = exportManagerService;
            _encryptService = encryptService;
            _licenseService = licenseService;
        }

        public string Generate(string order, string code, string expirionDate)
        {
            string license = null;

            string statusLicense = null;

            try
            {
                string[] decode = null;

                try
                {
                    decode = _encryptService.Decrypt(code, "L3@nTe$t").Split(';');
                }
                catch (Exception)
                {
                    if (string.IsNullOrEmpty(code))
                    {
                        return "Invalid keyCode";
                    }
                }
                string hostName = decode[0];

                string mac = decode[1];

                string typeLicense = decode[2];

                if (string.IsNullOrEmpty(hostName) && (string.IsNullOrEmpty(mac)) && (string.IsNullOrEmpty(typeLicense)))
                {
                    return "License error!";
                }
                license = typeLicense + ";" + hostName + ";" + mac + ";" + expirionDate;

                license = _encryptService.Cryptografy(license, "L3@nTe$t");

                statusLicense = _encryptService.Cryptografy(hostName + ";Active", "LeªNte§t");

                license = _exportManagerService.ExportLicenseXml(order, license, expirionDate, typeLicense, hostName, statusLicense, code);

                return license;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message.ToString();
            }
        }
    }
}
