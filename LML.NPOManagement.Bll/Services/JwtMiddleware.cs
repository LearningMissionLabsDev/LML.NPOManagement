using LML.NPOManagement.Common;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LML.NPOManagement.Bll.Services
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration configuration, IUserRepository userRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                await AttachUserToContext(context, token, configuration, userRepository);
                var user = context.Items["User"] as UserModel;
                if (user?.Account2Users.Count > 0)
                {
                    _ = int.TryParse(context.Request.Query["accountId"], out int accountId);
                    await SetAccountAndRoleContext(context, accountId, token, configuration, userRepository);
                }
            }
            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, string token, IConfiguration configuration, IUserRepository userRepository)
        {
            try
            {
                var user = await TokenCreationHelper.ValidateJwtToken(token, configuration, userRepository);
                context.Items["User"] = user;
            }
            catch
            {

            }
        }

        private async Task SetAccountAndRoleContext(HttpContext context, int accountId, string token, IConfiguration configuration, IUserRepository userRepository)
        {
            try
            {
                var user = await TokenCreationHelper.ValidateJwtToken(token, configuration, userRepository);
                if (user.Account2Users != null)
                {
                    var account2User = user.Account2Users.FirstOrDefault(acc => acc.AccountId == accountId);
                    context.Items["Account"] = account2User;
                }
            }
            catch
            {

            }
        }
    }
}