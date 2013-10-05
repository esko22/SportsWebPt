using System.Collections.Generic;
using System.Runtime.Serialization;

using ServiceStack.ServiceInterface.ServiceModel;

namespace SportsWebPt.Common.ServiceStack
{
    [KnownType(typeof(Notification))]
    [DataContract]
    public class ApiResponse<TResponse> 
    {
        public ApiResponse() { }

        public ApiResponse(TResponse responseContent)
        {
            ResponseStatus = new ResponseStatus();
            Notifications = new List<INotification<object>>();
            Response = responseContent;
        }

        [DataMember(IsRequired = true)]
        public virtual TResponse Response { get; set; }

        [DataMember(IsRequired = true)]
        public ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired = true)]
        public IList<INotification<object>> Notifications { get; set; }
    }
}
