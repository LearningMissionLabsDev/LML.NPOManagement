namespace LML.NPOManagement.Common
{
    public enum ServiceStatusCode
    {
        Success = 1,        
        UserNotFound = 2,  
        InvalidCredentials = 3,
        AccountNotFound = 4,
        UserInactive = 5,
        PreconditionRequired = 6,
        Conflict = 7,
        BadRequest = 8,
        InternalServerError = 9,
        Unauthorized = 10
    }
}