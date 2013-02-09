using ServiceStack.ServiceInterface.ServiceModel;

namespace SportsWebPt.Common.ServiceStack.Infrastructure
{

    /// <summary>
    /// All of your service Responses should inherity from this interface
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IRestResponse<TResult> : IHasResponseStatus, IHasNotifications
    {
        TResult Response { get; }
    }
}
