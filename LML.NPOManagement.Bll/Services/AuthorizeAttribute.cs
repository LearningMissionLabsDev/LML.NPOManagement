using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LML.NPOManagement.Bll.Services
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly int[] _roles;
        public AuthorizeAttribute()
        {
        }
        public AuthorizeAttribute(params int[] roles) 
        {
            _roles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"] as UserModel;
            
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

            if (!_roles.Any(role => user.Account2Users.Any(account => account.AccountRoleId == role)))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }       
    }
}
