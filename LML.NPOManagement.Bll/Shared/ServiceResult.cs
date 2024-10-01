using LML.NPOManagement.Common;

namespace LML.NPOManagement.Bll.Shared
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public ServiceStatusCode StatusCode { get; set; }

        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T> { IsSuccess = true, Data = data, StatusCode = ServiceStatusCode.Success };
        }

        public static ServiceResult<T> Failure(string errorMessage, ServiceStatusCode statusCode)
        {
            return new ServiceResult<T> { IsSuccess = false, ErrorMessage = errorMessage, StatusCode = statusCode };
        }
    }
}