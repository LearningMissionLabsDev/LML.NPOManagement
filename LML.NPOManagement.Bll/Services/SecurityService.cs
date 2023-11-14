using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Dal;
using LML.NPOManagement.Bll.Model;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using LML.NPOManagement.Dal.Models;
using BC = BCrypt.Net.BCrypt;

namespace LML.NPOManagement.Bll.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly INPOManagementContext _dbContext;
        private static List<ConnectionInformation> _connectsInfo = new List<ConnectionInformation>();

        public SecurityService(INPOManagementContext context)
        {
            _dbContext = context;
        }

        public async Task<string> CreateNewKey(ConnectionInformation connectInfo)
        {
            var result = await CreateRsaKey(connectInfo);
            if (result == true)
            {
                int prevIndex = _connectsInfo.FindIndex(conn => conn.Id == connectInfo.Id && conn.IP == connectInfo.IP && conn.Port == connectInfo.Port);
                if (prevIndex != -1)
                {
                    _connectsInfo.RemoveAt(prevIndex);
                }
                _connectsInfo.Add(connectInfo);
                return connectInfo.publicKey;
            }
            return null!;
        }

        public async Task<bool> VerifyPassword(string encryptedPassword, string hashedPassword, ConnectionInformation connectInfo)
        {
            var privateKey = await GetPrivateKey(connectInfo);
            var password = await Decrypt(encryptedPassword, privateKey);
            return BC.Verify(password, hashedPassword);
        }


        public async Task<string> Encrypt(string message, string publicKey)
        {
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(publicKey);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] encryptedBytes = rsa.Encrypt(messageBytes, RSAEncryptionPadding.OaepSHA512);
            string encryptedText = Convert.ToBase64String(encryptedBytes);
            return encryptedText;
        }

        public async Task<string> Decrypt(string message, string key)
        {
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(key);
            byte[] encryptedBytes = Convert.FromBase64String(message);
            byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA512);
            string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedText;
        }

        public async Task<bool> AddSecretMessage(Message message, ConnectionInformation connectInfo)
        {
            var privateKey = await GetPrivateKey(connectInfo);
            if (privateKey == null)
            {
                return false;
            }
            _dbContext.Messages.Add(message);
            var isSaved = await _dbContext.SaveChangesAsync();
            if (!Convert.ToBoolean(isSaved))
            {
                return false;
            }
            MessageKey messageKey = new MessageKey
            {
                MessageId = message.Id,
                PrivateKey = privateKey,
            };
            _dbContext.MessagesKeys.Add(messageKey);
            isSaved = await _dbContext.SaveChangesAsync();
            if (!Convert.ToBoolean(isSaved))
            {
                _dbContext.Messages.Remove(message);
                await _dbContext.SaveChangesAsync();
                return false;
            }
            return true;
        }

        private ConnectionInformation? GetConnection(ConnectionInformation connectInfo)
        {
            return _connectsInfo.Where(conn => conn.Id == connectInfo.Id && conn.IP == connectInfo.IP && conn.Port == connectInfo.Port).FirstOrDefault();
        }

        private async Task<bool> CreateRsaKey(ConnectionInformation connectInfo)
        {
            RsaKeyPairGenerator keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
            AsymmetricCipherKeyPair keyPair = keyPairGenerator.GenerateKeyPair();
            return UpdateKeys(keyPair, connectInfo);
        }

        //private async Task<bool> ReadRsaKey(ConnectionInformation connectInfo)
        //{
        //    StringReader privateKeyStringReader = new StringReader(privateRsaKey);
        //    PemReader pemReader = new PemReader(privateKeyStringReader);
        //    AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
        //    return UpdateKeys(keyPair, connectInfo);
        //}

        private bool UpdateKeys(AsymmetricCipherKeyPair keyPair, ConnectionInformation connectInfo)
        {
            using (StringWriter keyStringWriter = new StringWriter())
            {
                PemWriter pemWriter = new PemWriter(keyStringWriter);
                pemWriter.WriteObject(keyPair.Private);
                connectInfo.privateKey = keyStringWriter.ToString();
            }
            using (StringWriter keyStringWriter = new StringWriter())
            {
                PemWriter pemWriter = new PemWriter(keyStringWriter);
                pemWriter.WriteObject(keyPair.Public);
                connectInfo.publicKey = keyStringWriter.ToString();
            }
            return true;
        }

        private async Task<string> GetPrivateKey(ConnectionInformation connectInfo)
        {
            var foundConnection = GetConnection(connectInfo);
            if (foundConnection == null)
            {
                return null!;
            }
            _connectsInfo.Remove(foundConnection);
            return foundConnection.privateKey;
        }
    }
}
