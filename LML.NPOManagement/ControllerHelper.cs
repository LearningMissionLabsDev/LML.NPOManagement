using LML.NPOManagement.Bll.Shared;
using LML.NPOManagement.Common;
using Microsoft.AspNetCore.Mvc;

namespace LML.NPOManagement
{
    public static class ControllerHelper
    {
        public static ActionResult HandleServiceResult<T>(ControllerBase controller, ServiceResult<T> result)
        {
            if (result.IsSuccess)
            {
                return controller.Ok(result.Data);
            }

            return result.StatusCode switch
            {
                ServiceStatusCode.UserNotFound => controller.NotFound(result.ErrorMessage),
                ServiceStatusCode.InvalidCredentials => controller.Unauthorized(result.ErrorMessage),
                ServiceStatusCode.PreconditionRequired => controller.StatusCode(428, result.ErrorMessage),
                ServiceStatusCode.Conflict => controller.Conflict(result.ErrorMessage),
                ServiceStatusCode.BadRequest => controller.BadRequest(result.ErrorMessage),
                ServiceStatusCode.Unauthorized => controller.Unauthorized(result.ErrorMessage),
                _ => controller.StatusCode(500, "Internal Server Error")
            };
        }
    }
}

