using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Registration.LoginRegistration
{
    public class Login
    {

        //private void CreatePasswordHash(string password, out byte[] passwordHash)
        //{
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512())
        //    {
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}
        //private bool VerifyPasswordHash(string password, byte[] passwordHash)
        //{
        //    using (var hmac = new HMACSHA512(passwordHash))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        return computedHash.SequenceEqual(passwordHash);
        //    }
        //}
        public async Task<UserModel> Loging(string email, string pass)
        {
            try
            {
                var manager = new AuthDataManager();
                return await manager.Login(email, pass);
            }
            catch (Exception e)
            {
                ExceptionLogManager.LogException(e);
                return null;
            }
        }
    }
}
