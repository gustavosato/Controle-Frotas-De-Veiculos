
using System;
using System.Collections;
using System.Collections.Generic;

namespace ControleVeiculos.Domain.Services
{
    public interface IStringUtilityService : IDisposable
    {
        string RemoveSpecialCharacters(string text, string characterReplace = "");
        string RemoveNullCharacters(string text);
        string RandomString(string format);
        string RandomPassword(int length);
        bool GreaterThan(int expected, int actual);
        bool Equal(int expected, int actual);
    }
}
