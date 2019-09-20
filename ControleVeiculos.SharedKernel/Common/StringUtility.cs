using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ControleVeiculos.SharedKernel.Common
{
    public class StringUtility
    {
        public static string RemoverCaracteresEspeciais(string str)
        {
            string temp = Regex.Replace(str, "[^0-9a-zA-Z]+", "");

            return temp;
        }

        public static string RandomString(string format)
        {
            string result = string.Empty;
            Random random = new Random();
            const string alpha = "ABCDEFGHIJKLMNOPQRSTUVXZWY";
            const string numeric = "123456789";
            bool isNumeric;


                for (int f = 0; f < format.Length; f++)
                {
                    int temp;
                    isNumeric = int.TryParse(format[f].ToString(), out temp);

                    if(isNumeric)
                        result += new string(Enumerable.Repeat(numeric, 1).Select(x => x[random.Next(x.Length)]).ToArray());
                    else
                        result += new string(Enumerable.Repeat(alpha, 1).Select(x => x[random.Next(x.Length)]).ToArray());
                }
            

            return result.ToUpper();
        }

        public static bool GreaterThan(int expected, int actual)
        {
            return actual > expected;
        }

        public static bool Equal(int expected, int actual)
        {
            return expected == actual;
        }
    }
}
