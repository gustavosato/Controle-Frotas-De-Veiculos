using System;
using System.Linq;
using System.Text.RegularExpressions;
using ControleVeiculos.Domain.Services;
using System.Web;

namespace ControleVeiculos.ApplicationService
{
    public class StringUtilityService : BaseAppService, Domain.Services.IStringUtilityService
    {
        public string RemoveSpecialCharacters(string str, string characterReplace = "")
        {
            string temp = Regex.Replace(str, "[^0-9a-zA-Z]+", characterReplace);

            return temp;
        }

        public string RemoveNullCharacters(string Text)
        {
            
            return Text.Replace("\0", string.Empty);
        }

        public string RandomString(string format)
        {
            string result = string.Empty;

            Random random = new Random();

            const string alpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVXZWY";

            const string numeric = "0123456789";

            bool isNumeric;
            
            for (int f = 0; f < format.Length; f++)
            {
                int temp;

                isNumeric = int.TryParse(format[f].ToString(), out temp);

                if (isNumeric)
                    result += new string(Enumerable.Repeat(numeric, 1).Select(x => x[random.Next(x.Length)]).ToArray());
                else
                    result += new string(Enumerable.Repeat(alpha, 1).Select(x => x[random.Next(x.Length)]).ToArray());
            }

            return result.ToString();
        }

        public string RandomPassword(int length)
        {
            string result = string.Empty;

            Random random = new Random();

            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVXZWY0123456789!@#$%&*?-+=";

            for (int f = 0; f < length; f++)
            {
                result += new string(Enumerable.Repeat(characters, 1).Select(x => x[random.Next(x.Length)]).ToArray());
            }

            return result.ToString();
        }

        public bool GreaterThan(int expected, int actual)
        {
            return actual > expected;
        }

        public bool Equal(int expected, int actual)
        {
            return expected == actual;
        }
    }
}
