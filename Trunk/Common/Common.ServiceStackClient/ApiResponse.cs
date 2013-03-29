using System.Collections.Generic;

using ServiceStack.ServiceInterface.ServiceModel;

namespace SportsWebPt.Common.ServiceStackClient
{
    public class ApiResponse<TResponse>
     where TResponse : class
    {
        public virtual TResponse Resource { get; set; }

        public ResponseStatus ResponseStatus { get; set; }

        public IList<INotification<object>> Notifications { get; set; }
    }

    public interface INotification<TItem>
    {
        string Type { get; set; }

        TItem Item { get; set; }
    }
}
