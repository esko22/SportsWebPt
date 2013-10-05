using System;

using ServiceStack.ServiceHost;

namespace SportsWebPt.Common.ServiceStack
{
    [Route("/ping")]
    public class PingRequest : IReturn<ApiResponse<String>> 
    {
    }
}
