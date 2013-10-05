using System.Runtime.Serialization;

namespace SportsWebPt.Common.ServiceStack
{
    [DataContract]
    public class ErrorResponse : ApiResponse<object>
    {
        public ErrorResponse(string message, string errorCode)
            : base(null)
        {
            base.ResponseStatus.Message = message;
            base.ResponseStatus.ErrorCode = errorCode;
        }
    }
}
