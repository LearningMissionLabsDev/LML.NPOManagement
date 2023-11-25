using System;
using System.Security.Cryptography;

namespace LML.NPOManagement.Bll.Utilities
{
	public class RSA
	{
        public static string Encrypt(string publicKey, string message)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                var encryptedBytes = rsa.Encrypt(System.Text.Encoding.UTF8.GetBytes(message), true);
                return Convert.ToBase64String(encryptedBytes);
            }
        }


        public static string Decrypt(string privateKey, string encryptedMessage)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);

                var encryptedBytes = Convert.FromBase64String(encryptedMessage);
                var decryptedBytes = rsa.Decrypt(encryptedBytes, true);

                return System.Text.Encoding.UTF8.GetString(decryptedBytes);
            }
        }

    }
}

