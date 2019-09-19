
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lean.Test.Cloud.Domain.Services
{
    public interface IMailService : IDisposable
    {
        string Send(string email, string password, string mailFrom, string mailTo, string subject, string body, string attachment);
    }
}
