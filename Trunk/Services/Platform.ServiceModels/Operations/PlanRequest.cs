using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/plans", "GET")]
    public class PlanListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<PlanDto, BasicSortBy>> 
    {}

    [Route("/plans", "POST")]
    public class CreatePlanRequest : PlanDto, IReturn<ApiResponse<PlanDto>>
    { }

    [Route("/plans/{id}", "PUT")]
    public class UpdatePlanRequest : PlanDto, IReturn<ApiResponse<PlanDto>>
    { }

    [Route("/plans/{id}", "GET")]
    public class PlanRequest : AbstractResourceRequest, IReturn<ApiResponse<PlanDto>>
    {}

}
