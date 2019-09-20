using ControleVeiculos.Domain.Command.Licenses;
using ControleVeiculos.Domain.Entities.Licenses;
using System;

namespace ControleVeiculos.Domain.Services
{
    public interface ILicenseGeneratorService : IDisposable
    {
        string Generate(string order, string code, string endDate);
    }
}
