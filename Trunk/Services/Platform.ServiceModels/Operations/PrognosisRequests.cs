using System;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/prognoses", "GET")]
    public class PrognosisListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<PrognosisDto, BasicSortBy>>
    { }

    [Route("/prognoses", "POST")]
    public class CreatePrognosisRequest : PrognosisDto, IReturn<ApiResponse<PrognosisDto>>
    {
    }

    [Route("/prognoses/{id}", "PUT")]
    public class UpdatePrognosisRequest : PrognosisDto, IReturn<ApiResponse<PrognosisDto>>
    {
    }

    [Route("/prognoses/{id}", "GET")]
    public class PrognosisRequest : AbstractResourceRequest, IReturn<ApiResponse<PrognosisDto>>
    { }
}
