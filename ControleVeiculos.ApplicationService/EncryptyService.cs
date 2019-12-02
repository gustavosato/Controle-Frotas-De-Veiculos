//using System.Security.Cryptography;
//using System.IO;
//using System.Text;
//using System;
//using ControleVeiculos.Domain.Services;

//namespace ControleVeiculos.ApplicationService
//{
//    public class EncryptService : BaseAppService, IEncryptyService
//    {
//        private readonly IStringUtilityService _stringUtilityrService;

//        public EncryptService(IStringUtilityService stringUtilityrService)
//        {
//            _stringUtilityrService = stringUtilityrService;
//        }

//        public string GetHash(string password)
//        {
//            byte[] tmpSource;

//            byte[] tmpHash;

//            string Output = null;

//            try
//            {
//                tmpSource = ASCIIEncoding.ASCII.GetBytes(password);

//                tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

//                for (var i = 0; i <= tmpHash.Length - 1; i++)

//                    Output += (tmpHash[i].ToString("X2"));

//                return Output;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }

//        public string Cryptografy(string text, string encriptKey)
//        {
//            byte[] bytValue;

//            byte[] bytKey;

//            byte[] bytEncoded = null;

//            byte[] bytIV = { 121, 241, 10, 1, 132, 74, 11, 39, 255, 91, 45, 78, 14, 211, 22, 62 };

//            int intLength;

//            int intRemaining;

//            MemoryStream objMemoryStream = new MemoryStream();

//            CryptoStream objCryptoStream;

//            RijndaelManaged objRijndaelManaged;

//            text = _stringUtilityrService.RemoveNullCharacters(text);

//            bytValue = Encoding.ASCII.GetBytes(text.ToCharArray());

//            intLength = encriptKey.Length;

//            if (intLength >= 8)
//            {
//                encriptKey = (encriptKey).Substring(0, 8);
//            }
//            else
//            {
//                intLength = encriptKey.Length;

//                intRemaining = 8 - intLength;

//                string strDup = new string('X', intRemaining);

//                encriptKey += strDup;
//            }
//            bytKey = Encoding.ASCII.GetBytes(encriptKey.ToCharArray());

//            objRijndaelManaged = new RijndaelManaged();

//            try
//            {
//                objCryptoStream = new CryptoStream(objMemoryStream, objRijndaelManaged.CreateEncryptor(bytKey, bytIV), CryptoStreamMode.Write);

//                objCryptoStream.Write(bytValue, 0, bytValue.Length);

//                objCryptoStream.FlushFinalBlock();

//                bytEncoded = objMemoryStream.ToArray();

//                objMemoryStream.Close();

//                objCryptoStream.Close();
//            }
//            catch
//            {
//            }
//            return Convert.ToBase64String(bytEncoded);
//        }

//        public string Decrypt(string Text, string encryptKey)
//        {
//            byte[] bytDataToBeDecrypted;

//            byte[] bytTemp;

//            byte[] bytIV = { 121, 241, 10, 1, 132, 74, 11, 39, 255, 91, 45, 78, 14, 211, 22, 62 };

//            RijndaelManaged objRijndaelManaged = new RijndaelManaged();

//            MemoryStream objMemoryStream;

//            CryptoStream objCryptoStream;

//            byte[] bytDecryptionKey;

//            int intLength;

//            int intRemaining;

//            bytDataToBeDecrypted = Convert.FromBase64String(Text);

//            intLength = encryptKey.Length;

//            if (intLength >= 8)
//            {
//                encryptKey = encryptKey.Substring(0, 8);
//            }
//            else
//            {
//                intLength = encryptKey.Length;

//                intRemaining = 8 - intLength;

//                string strDup = new string('X', intRemaining);

//                encryptKey += strDup;
//            }

//            bytDecryptionKey = Encoding.ASCII.GetBytes(encryptKey.ToCharArray());

//            bytTemp = new byte[bytDataToBeDecrypted.Length + 1];

//            objMemoryStream = new MemoryStream(bytDataToBeDecrypted);

//            try
//            {
//                objCryptoStream = new CryptoStream(objMemoryStream, objRijndaelManaged.CreateDecryptor(bytDecryptionKey, bytIV), CryptoStreamMode.Read);

//                objCryptoStream.Read(bytTemp, 0, bytTemp.Length);

//                objMemoryStream.Close();

//                objCryptoStream.Close();
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//            return _stringUtilityrService.RemoveNullCharacters(Encoding.ASCII.GetString(bytTemp));
//        }
//    }
//}