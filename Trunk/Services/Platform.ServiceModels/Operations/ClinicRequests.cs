using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.ServiceHost;

using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels.Operations
{
    [Route("/clinics/{id}/plans")]
    public class ClinicPlanRequest : AbstractResourceRequest, IReturn<ApiResponse<PlanDto>>
    { }
}
