using Lean.Test.Cloud.Domain.Command.Licenses;
using Lean.Test.Cloud.Domain.Entities.Licenses;
using System;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface ILicenseGeneratorService : IDisposable
    {
        string Generate(string order, string code, string endDate);
    }
}
