using System;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/causes", "GET")]
    public class CauseListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<CauseDto,BasicSortBy>>
    {
    }

    [Route("/causes", "POST")]
    public class CreateCauseRequest : CauseDto, IReturn<ApiResponse<CauseDto>>
    {
    }

    [Route("/causes/{id}", "PUT")]
    public class UpdateCauseRequest : CauseDto, IReturn<ApiResponse<CauseDto>>
    {
    }

    [Route("/causes/{id}", "GET")]
    public class CauseRequest : AbstractResourceRequest, IReturn<ApiResponse<CauseDto>>
    {}
}
