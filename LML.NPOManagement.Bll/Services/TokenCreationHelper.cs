using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LML.NPOManagement.Bll.Services
{
    public static class TokenCreationHelper
    {
        private static SymmetricSecurityKey _signingKey = null;

        private static SymmetricSecurityKey GetSigningKey(IConfiguration configuration)
        {
            if (_signingKey == null)
            {
                var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value);
                _signingKey = new SymmetricSecurityKey(key);
            }

            return _signingKey;
        }

        public static string GenerateJwtToken(UserModel user, IConfiguration configuration, IUserRepository userRepository)
        {
            string token = "";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value);
            if (user.Account2Users.Count == 1)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("AccountId",user.Account2Users.First().AccountId.ToString()),
                    new Claim("AccountRoleId",user.Account2Users.First().AccountRoleId.ToString())
                }),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(configuration.GetSection("AppSettings:TokenExpiration").Value)),
                    SigningCredentials = new SigningCredentials(GetSigningKey(configuration), SecurityAlgorithms.HmacSha256Signature)
                };
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);
                token = tokenHandler.WriteToken(createdToken);
                return token;
            }
            else if (user.Account2Users.Count > 1)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                        new Claim("Id", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(configuration.GetSection("AppSettings:TokenExpiration").Value)),
                    SigningCredentials = new SigningCredentials(GetSigningKey(configuration), SecurityAlgorithms.HmacSha256Signature)
                };
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);
                token = tokenHandler.WriteToken(createdToken);
                return token;
            }
            return token;
        }

        public static string GenerateJwtTokenAccount(AccountModel accountModel, IConfiguration configuration)
        {
            string token = "";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value);
            if (accountModel.Account2Users.Count == 1)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                    new Claim("Id",accountModel.Id.ToString()),
                }),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(configuration.GetSection("AppSettings:TokenExpiration").Value)),
                    SigningCredentials = new SigningCredentials(GetSigningKey(configuration), SecurityAlgorithms.HmacSha256Signature)
                };
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);
                token = tokenHandler.WriteToken(createdToken);
            }
            return token;
        }


        public static async Task<AccountModel> ValidateJwtTokenAccount(string accountToken, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value);

            try
            {
                tokenHandler.ValidateToken(accountToken, new TokenValidationParameters
                {
                    /* rg: added */
                    RequireAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = false,
                    /*<<<*/
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = GetSigningKey(configuration),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                AccountModel accountModel = new AccountModel();
                accountModel.Id = Convert.ToInt16(jwtToken.Claims.First(x => x.Type == "Id").Value);
                return accountModel;
            }
            catch (Exception exp)
            {
                // if validation fails then return null
                return null;
            }
        }

        public static async Task<UserModel> ValidateJwtToken(string token, IConfiguration configuration, IUserRepository userRepository)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    /* rg: added */
                    RequireAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = false,
                    /*<<<*/
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = GetSigningKey(configuration),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                UserModel userModel = new UserModel();
                userModel.Id = Convert.ToInt16(jwtToken.Claims.First(x => x.Type == "Id").Value);
                var accounts = await userRepository.GetUsersInfoAccount(userModel.Id);
                if (accounts == null)
                {
                    return null;
                }
                foreach (var account in accounts)
                {
                    userModel.Account2Users.Add(account);
                }
                return userModel;
            }
            catch (Exception exp)
            {
                // if validation fails then return null
                return null;
            }
        }
    }

}


