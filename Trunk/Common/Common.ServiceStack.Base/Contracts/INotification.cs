
namespace SportsWebPt.Common.ServiceStack
{
    public interface INotification<TItem>
    {
        string Type { get; set; }

        TItem Item { get; set; }
    }
}
