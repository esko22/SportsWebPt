using System.Collections.Generic;
using System.Runtime.Serialization;

using ServiceStack.ServiceInterface.ServiceModel;

namespace SportsWebPt.Common.ServiceStack.Infrastructure
{
    [KnownType(typeof(Notification))]
    [DataContract]
    public class ApiResponse<TTypeOfResource> : IRestResponse<TTypeOfResource>
    {
        public ApiResponse() { }

        public ApiResponse(TTypeOfResource responseContent)
        {
            ResponseStatus = new ResponseStatus();
            Notifications = new List<INotification<object>>();
            Resource = responseContent;
        }

        [DataMember(IsRequired = true)]
        public virtual TTypeOfResource Resource { get; set; }

        [DataMember(IsRequired = true)]
        public ResponseStatus ResponseStatus { get; set; }

        [DataMember(IsRequired = true)]
        public IList<INotification<object>> Notifications { get; set; }
    }
}
