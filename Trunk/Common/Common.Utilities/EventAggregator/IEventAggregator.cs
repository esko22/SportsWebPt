
namespace SportsWebPt.Common.Utilities
{
    public interface IEventAggregator
    {
        TEventType GetEvent<TEventType>() where TEventType : BaseEvent;
    }
}