using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Repositories;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
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

        public static string GenerateJwtToken(UserModel user, IConfiguration configuration, IUserRepository userRepository, int accountId=0)
        {
            string token = "";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value);
            var adminAccount = user.Account2Users.FirstOrDefault(acc => acc.AccountId == 1);
            int currentRoleId = -1;

            if (adminAccount != null)
            {
                if (adminAccount.AccountRoleId == (int)UserAccountRoleEnum.Admin)
                {
                    adminAccount.AccountRoleId = (int)UserAccountRoleEnum.SysAdmin;
                }
                currentRoleId = adminAccount.AccountRoleId;
            }

            if (accountId <= 0)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.Id.ToString()),
                }),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(configuration.GetSection("AppSettings:TokenExpiration").Value)),
                    SigningCredentials = new SigningCredentials(GetSigningKey(configuration), SecurityAlgorithms.HmacSha256Signature)
                };
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);
                token = tokenHandler.WriteToken(createdToken);             
            }
            else
            {
                var account2User = user.Account2Users.Where(accId => accId.AccountId == accountId).FirstOrDefault();
                if(account2User == null)
                {
                    return null;
                }
                var roleId = account2User.AccountRoleId;
                
                if(currentRoleId != -1)
                {
                    roleId = currentRoleId;
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("AccountId", accountId.ToString()),
                    new Claim("AccountRoleId", roleId.ToString())
                }),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(configuration.GetSection("AppSettings:TokenExpiration").Value)),
                    SigningCredentials = new SigningCredentials(GetSigningKey(configuration), SecurityAlgorithms.HmacSha256Signature)
                };
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);
                token = tokenHandler.WriteToken(createdToken);
            }
            return token;
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
                    return userModel;
                }
                else
                {
                    foreach (var account in accounts)
                    {
                        userModel.Account2Users.Add(account);
                    }
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


