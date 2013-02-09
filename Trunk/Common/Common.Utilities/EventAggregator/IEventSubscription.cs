using System;

namespace SportsWebPt.Common.Utilities
{
    public interface IEventSubscription
    {
        SubscriptionToken SubscriptionToken
        {
            get;
            set;
        }

        Action<object[]> GetExecutionStrategy();
    }
}