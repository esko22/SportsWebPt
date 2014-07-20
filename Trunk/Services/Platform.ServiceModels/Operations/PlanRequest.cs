using System;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/plans", "GET")]
    public class PlanListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<PlanDto, BasicSortBy>>
    {
    }

    [Route("/plans/brief", "GET")]
    public class BriefPlanListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<BriefPlanDto, BasicSortBy>>
    {
        #region Propeties

        public int ClinicId { get; set; }

        public Boolean? IsPublic { get; set; } 

        #endregion
    }


    [Route("/plans", "POST")]
    public class CreatePlanRequest : PlanDto, IReturn<ApiResponse<PlanDto>>
    {
    }

    [Route("/plans/{id}", "PUT")]
    public class UpdatePlanRequest : PlanDto, IReturn<ApiResponse<PlanDto>>
    {
    }

    [Route("/plans/{id}", "GET")]
    public class PlanRequest : AbstractResourceRequest, IReturn<ApiResponse<PlanDto>>
    {
    }

    [Route("/clinics/{id}/plans")]
    public class ClinicPlanRequest : AbstractResourceRequest, IReturn<ApiResponse<PlanDto>>
    {}

}
