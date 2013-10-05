using System;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/bodyregions", "GET")]
    public class BodyRegionListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<BodyRegionDto, BasicSortBy>>
    {
    }

    [Route("/bodyregions", "POST")]
    public class CreateBodyRegionRequest : BodyRegionDto, IReturn<ApiResponse<BodyRegionDto>>
    {
    }

    [Route("/bodyregions", "PUT")]
    public class UpdateBodyRegionRequest : BodyRegionDto, IReturn<ApiResponse<BodyRegionDto>>
    {
    }

    [Route("/bodyregions/{id}", "GET")]
    public class BodyRegionRequest : AbstractResourceRequest, IReturn<ApiResponse<BodyRegionDto>>
    {
    }

}
