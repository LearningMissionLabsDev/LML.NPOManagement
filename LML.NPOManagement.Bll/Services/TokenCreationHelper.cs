using LML.NPOManagement.Bll.Model;
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
        public static string GenerateJwtToken(UserModel user, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(configuration.GetSection("AppSettings:TokenExpiration").Value)),
                SigningCredentials = new SigningCredentials(GetSigningKey(configuration), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static UserModel ValidateJwtToken(string token, IConfiguration configuration)
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

                // if validation is successful then return UserId from JWT token 

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
