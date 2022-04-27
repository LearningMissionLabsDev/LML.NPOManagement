using LML.NPOManagement.Bll.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LML.NPOManagement.Bll.Services
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private IList<UserTypeEnum> _allowedUserTypes;
        public AuthorizeAttribute(params UserTypeEnum[] userType)
        {
            _allowedUserTypes = userType ?? new UserTypeEnum[] { };
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
        //public void OnAuthorization(AuthorizationFilterContext context)
        //{
        //    // skip authorization if action is decorated with [AllowAnonymous] attribute
        //    var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        //    if (allowAnonymous)
        //        return;

        //    // authorization
        //    var user = context.HttpContext.Items["User"] as UserModel;



        //    foreach (var item in user.UserTypes)
        //    {
        //        if (user == null || (item.UserTypeEnum != UserTypeEnum.Undefined && !_allowedUserTypes.Contains(item.UserTypeEnum)))
        //        {
        //            // not logged in or role not authorized
        //            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        //        }
        //    }



        //}
    }
}
