using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PresentationLayer.App_Code
{
    public class Utilities
    {
        public static string Encrypt(string plainText)
        {
            string EncryptionKey = "3a5c1c4e81d7eb133a5c1c4e81d7eb13";
            System.Security.Cryptography.Rijndael tdes = System.Security.Cryptography.Rijndael.Create();
            tdes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            tdes.Mode = System.Security.Cryptography.CipherMode.ECB;
            tdes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            System.Security.Cryptography.ICryptoTransform crypt = tdes.CreateEncryptor();
            byte[] plain = Encoding.UTF8.GetBytes(plainText);
            byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
            plainText = Convert.ToBase64String(cipher);
            return plainText;
        }
    }
}