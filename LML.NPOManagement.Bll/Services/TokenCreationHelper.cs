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
      
        public static string GenerateJwtToken(UserModel user, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Logging:SecretKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.Id.ToString()),
                   
                    
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16( configuration.GetSection("Logging:TokenExpiration").Value)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static UserModel ValidateJwtToken(string token, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Logging:SecretKey").Value);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
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
            catch (System.Exception exp)
            {
                // if validation fails then return null
                return null;
            }
        }
    }
}
