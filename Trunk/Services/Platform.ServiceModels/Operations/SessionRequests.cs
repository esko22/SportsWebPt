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

    [Route("/sessions", "PUT")]
    public class UpdateSessionRequest : SessionDto, IReturn<ApiResponse<SessionDto>>
    {
    }

    [Route("/sessions/{id}/plans", "POST")]
    public class CreateSessionPlanRequest : SessionDto, IReturn<ApiResponse<Boolean>>
    {
        #region Properties

        public int[] PlanIds { get; set; }

        #endregion
    }

    [Route("/sessions/{id}", "GET")]
    public class SessionRequest : AbstractResourceRequest, IReturn<ApiResponse<SessionDto>>
    {
        #region Properties

        public String TherapistId { get; set; }

        #endregion
    }



}
