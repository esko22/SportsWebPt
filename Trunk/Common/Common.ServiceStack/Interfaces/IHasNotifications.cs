using System.Collections.Generic;

namespace SportsWebPt.Common.ServiceStack.Infrastructure
{
    public interface IHasNotifications
    {
        IList<INotification<object>> Notifications { get; set; }
    }
}
