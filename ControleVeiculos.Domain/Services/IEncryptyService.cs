
using System;
using System.Collections;
using System.Collections.Generic;
namespace ControleVeiculos.Domain.Services
{
    public interface IEncryptyService : IDisposable
    {
        string GetHash(string password);
        string Cryptografy(string text, string encriptKey);
        string Decrypt(string Text, string encryptKey);
    }
}
