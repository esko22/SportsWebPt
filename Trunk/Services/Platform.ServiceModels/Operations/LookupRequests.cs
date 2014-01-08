using System;
using ServiceStack.ServiceHost;
using SportsWebPt.Common.ServiceStack;

namespace SportsWebPt.Platform.ServiceModels
{
    [Route("/signfilters", "GET")]
    public class SignFilterListRequest : AbstractResourceListRequest, IReturn<ApiListResponse<SignFilterDto, BasicSortBy>>
    { }

}
