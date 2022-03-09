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
        public async Task Invoke(HttpContext context, IConfiguration configuration)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                await AttachAccountToContext(context, token, configuration);
            }
            await _next(context);
        }
        private async Task AttachAccountToContext(HttpContext context, string token, IConfiguration configuration)
        {
            try
            {

                var user = TokenCreationHelper.ValidateJwtToken(token, configuration);
                // on successful jwt validation attach UserId to context
                context.Items["User"] = user;
            }
            catch
            {
                // if jwt validation fails then do nothing 
            }
        }
    }
}
