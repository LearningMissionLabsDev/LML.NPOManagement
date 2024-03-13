using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;

namespace LML.NPOManagement.Bll.Services
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;
        public AuthorizeAttribute()
        {
        }
        public AuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"] as UserModel;
            var rol = new List<UserAccountRoleEnum>();
       
            for (int i = 0; i < _roles.Length; i++)
            {
              var userRole = (UserAccountRoleEnum)Enum.Parse(typeof(UserAccountRoleEnum), _roles[i]);
                rol.Add(userRole);
            }
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else if (!rol.Any(role => user.Account2Users.Any(account => account.AccountRoleId == (int)role)))
            {
                context.Result = new JsonResult(new { message = "Access denied" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}
