using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{

    [Route("/sessions", "POST")]
    public class CreateSessionRequest : SessionDto, IReturn<ApiResponse<SessionDto>>
    {
    }

    [Route("/sessions/{id}", "GET")]
    public class SessionRequest : AbstractResourceRequest, IReturn<ApiResponse<SessionDto>>
    {
    }

}
