using System;

namespace Portal.Web.Models
{
    /// <summary>
    /// Application API calls response exception
    /// </summary>
    public class ApiResponseException : Exception
    {
        public new string Message { get; set; }
    }


    /// <summary>
    /// Response bad-request result data that after API calls may be occured. 
    /// </summary>
    public class ResponseErrorModel
    {
        public ApiResponseException Error { get; set; }
    }
}
