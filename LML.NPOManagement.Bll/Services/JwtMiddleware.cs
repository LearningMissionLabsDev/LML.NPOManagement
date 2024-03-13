using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace LML.NPOManagement.Bll.Services
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next) // <- error this
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration configuration, IUserRepository userRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var accountToken = context.Request.Headers["AccountAuthorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                await AttachAccountToContext(context, token, accountToken, configuration, userRepository);
            }
            await _next(context);
        }
        //--------------------

        //--------------------
        private async Task AttachAccountToContext(HttpContext context, string token, string accountToken, IConfiguration configuration, IUserRepository userRepository)
        {
            try
            {
                var user = await TokenCreationHelper.ValidateJwtToken(token, configuration, userRepository);
                var account = await TokenCreationHelper.ValidateJwtTokenAccount(accountToken, configuration);

                context.Items["User"] = user;
                context.Items["Account"] = account;
            }
            catch
            {

            }
        }
    }
}
