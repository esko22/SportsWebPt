
namespace SportsWebPt.Common.ServiceStack.Infrastructure
{
    public interface INotification<TItem>
    {
        string Type { get; set; }

        TItem Item { get; set; }
    }
}
