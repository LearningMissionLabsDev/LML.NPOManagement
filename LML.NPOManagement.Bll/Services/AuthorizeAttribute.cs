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
        private readonly int _role;
        public AuthorizeAttribute()
        {
        }
        public AuthorizeAttribute(int role)
        {
            _role = role;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"] as UserModel;
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            var account = context.HttpContext.Items["Account"] as Account2UserModel;
            if (account == null)
            {
                context.Result = new JsonResult(new { message = "Access denied" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
            var userRole = account?.AccountRoleId;

            if ((userRole & _role) != userRole)
            {
                context.Result = new JsonResult(new { message = "Access denied" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}